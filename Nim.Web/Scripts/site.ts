module Nim.Web.Home {
    export class Index {
        private form: JQuery;

        constructor(options) {
            // Initialize the instance from the options provided.
            var defaults = {
            };
            var settings = $.extend(true, {}, defaults, options);
            this.form = settings.form;

            this.form.on('click', '#btnPlay', (e) => {
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
                this.setButtons();
            });

            this.form.on('click', '.heap-remove', (e) => {
                var heapRemove = $(e.currentTarget);
                var remove = Number(heapRemove.attr('data-remove'));
                var heapNumber = Number(heapRemove.closest('.heap-row').attr('data-heap-number')) + 1;
                var heapSize = heapRemove.closest('.heap-row').find('.heap-size');

                this.log('Player removed ' + remove + ' from heap ' + heapNumber + '.');

                heapSize.val(Number(heapSize.val()) - remove);

                this.setButtons();
                this.form.find('.heap-remove').prop('disabled', true);

                setTimeout(() => this.computerMove(), 1000);
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

        setButtons() {
            $.each(this.form.find('.heap-row'), (indexInArray, value) => {
                var heapRow = $(value);
                heapRow.find('.heap-buttons').empty();
                for (var i = Number(heapRow.find('.heap-size').val()); i > 0; i--) {
                    heapRow.find('.heap-buttons').append(
                        $('<input type="button" class="btn btn-primary heap-remove">')
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
                this.setButtons();
                this.form.find('.heap-remove').prop('disabled', false);
            }).fail((data, textStatus, jqXHR) => {
            }).always(() => {
            });
        };
    }
}