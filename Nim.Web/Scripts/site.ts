module Nim.Web.Home {
    export class Index {
        private form: JQuery;

        constructor(options) {
            // Initialize the instance from the options provided.
            var defaults = {
            };
            var settings = $.extend(true, {}, defaults, options);
            this.form = settings.form;

            this.form.on('click', '.btn-play', (e) => {
                this.log('Starting the game! Player goes first.');
                $(e.currentTarget)
                    .blur()
                    .prop('disabled', true)
                    .removeClass('btn-primary')
                    .addClass('btn-secondary')
                    .val('Game in Progress');
                this.form
                    .find('.heap-size')
                    .prop('readonly', true);
                this.form
                    .find('.btn-hint')
                    .show();
                this.setButtons(true);
            });

            this.form.on('click', '.btn-remove', (e) => {
                var heapRemove = $(e.currentTarget);
                var remove = Number(heapRemove.attr('data-remove'));
                var heapNumber = Number(heapRemove.closest('.heap-row').attr('data-heap-number')) + 1;
                var heapSize = heapRemove.closest('.heap-row').find('.heap-size');

                this.log('Player removed ' + remove + ' from heap ' + heapNumber + '.');

                heapSize.val(Number(heapSize.val()) - remove);

                this.setButtons(false);
                this.form.find('.btn-remove').prop('disabled', true);

                var heapSizeSum = 0;
                $.each(this.form.find('.heap-size'), (indexInArray, value) => {
                    heapSizeSum += Number($(value).val());
                });
                if (heapSizeSum > 0) {
                    setTimeout(() => this.computerMove(), 1000);
                }
            });

            this.form.on('click', '.btn-hint', (e) => {
                $(e.currentTarget).blur();
                var hint = this.form.find('.hint');

                var heaps = [];
                $.each(this.form.find('.heap-size'), (indexInArray, value) => {
                    heaps.push(Number($(value).val()));
                });
                var data = JSON.stringify(heaps);
                $.ajax({
                    url: '/api/nextmove',
                    method: 'POST',
                    contentType: 'application/json',
                    data: data
                }).done((data, textStatus, jqXHR) => {
                    // No move is returned for a losing position.
                    // In that case, choose a random heap from those with at least one object left and choose a random number to remove.
                    if (Object.keys(data).length === 0) {
                        hint.val('Sorry, I can\'t help you.');
                    }
                    else {
                        hint.val('Try removing ' + data.number + ' from heap ' + (data.heap + 1) + '.');
                    }
                    
                    hint.fadeIn();
                    setTimeout(() => hint.fadeOut(), 1000);
                }).fail((data, textStatus, jqXHR) => {
                }).always(() => {
                });                
            });
        };

        log(text: string) {
            var textarea = this.form.find('textarea');
            if (textarea.val() === '') {
                textarea.val(text);
            }
            else {
                textarea.val(textarea.val() + '\n' + text);
            }
            textarea[0].scrollTop = textarea[0].scrollHeight;
        }

        setButtons(isPlayersTurn : boolean) {
            var heapSizeSum = 0;
            $.each(this.form.find('.heap-size'), (indexInArray, value) => {
                heapSizeSum += Number($(value).val());
            });
            if (heapSizeSum === 0) {
                this.log((isPlayersTurn ? 'Player' : 'Computer') + ' wins!');
                this.form.find('.btn-play').val('Game Over');
            }

            $.each(this.form.find('.heap-row'), (indexInArray, value) => {
                var heapRow = $(value);
                heapRow.find('.heap-buttons').empty();
                for (var i = Number(heapRow.find('.heap-size').val()); i > 0; i--) {
                    heapRow.find('.heap-buttons').append(
                        $('<input type="button" class="btn btn-primary btn-remove">')
                            .prop('value', i)
                            .attr('data-remove', i)
                    );
                }
                heapRow.find('.heap-form-group').show();
            });
        };

        computerMove() {
            var heaps = [];
            $.each(this.form.find('.heap-size'), (indexInArray, value) => {
                heaps.push(Number($(value).val()));
            });
            var data = JSON.stringify(heaps);

            $.ajax({
                url: '/api/nextmove',
                method: 'POST',
                contentType: 'application/json',
                data: data
            }).done((data, textStatus, jqXHR) => {
                // No move is returned for a losing position.
                // In that case, choose a random heap from those with at least one object left and choose a random number to remove.
                if (Object.keys(data).length === 0) {
                    var heapsInPlay = $.map(heaps, (elementOfArray, indexInArray) => {
                        return elementOfArray > 0 ? indexInArray: null;
                    });
                    var randomHeap = heapsInPlay[Math.floor(Math.random() * heapsInPlay.length)];
                    var randomNumber = Math.floor(Math.random() * (heaps[randomHeap] - 1)) + 1;
                    data = {
                        heap: randomHeap,
                        number: randomNumber
                    };
                }
                this.log('Computer removed ' + data.number + ' from heap ' + (data.heap + 1) + '.');
                var heapSize = this.form.find('.heap-row[data-heap-number=' + data.heap + '] .heap-size');
                heapSize.val(Number(heapSize.val()) - data.number);
                this.setButtons(true);
                this.form.find('.btn-remove').prop('disabled', false);
            }).fail((data, textStatus, jqXHR) => {
            }).always(() => {
            });
        };
    }
}