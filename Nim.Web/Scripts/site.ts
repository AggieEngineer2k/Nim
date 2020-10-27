module Nim.Web.Home {
    export class Index {
        private form: JQuery;

        constructor(options) {
            // Initialize the instance from the options provided.
            let defaults = { };
            let settings = $.extend(true, { }, defaults, options);
            this.form = settings.form;

            // Register form event handlers.
            this.form.on('click', '.btn-play', this.btnPlayClick);
            this.form.on('click', '.btn-remove', this.btnRemoveClick);
            this.form.on('click', '.btn-hint', this.btnHintClick);
        };

        // Adds a message to the log window.
        log = (text: string) => {
            let textarea = this.form.find('textarea');
            if (textarea.val() === '') {
                textarea.val(text);
            }
            else {
                textarea.val(textarea.val() + '\n' + text);
            }
            textarea[0].scrollTop = textarea[0].scrollHeight;
        }

        // Starts the game.
        btnPlayClick = (e) => {
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
            this.refreshButtons(true);
        };

        // Removes a number of objects from a heap.
        btnRemoveClick = (e) => {
            let heapRemove = $(e.currentTarget);
            let remove = Number(heapRemove.attr('data-remove'));
            let heapNumber = Number(heapRemove.closest('.heap-row').attr('data-heap-number')) + 1;
            let heapSize = heapRemove.closest('.heap-row').find('.heap-size');

            this.log('Player removed ' + remove + ' from heap ' + heapNumber + '.');

            heapSize.val(Number(heapSize.val()) - remove);

            this.refreshButtons(false);
            this.form.find('.btn-remove').prop('disabled', true);

            let heapSizeSum = 0;
            $.each(this.form.find('.heap-size'), (indexInArray, value) => {
                heapSizeSum += Number($(value).val());
            });
            if (heapSizeSum > 0) {
                setTimeout(() => this.computerMove(), 1000);
            }
        };

        // Refreshes the buttons that remove objects from heaps.
        refreshButtons = (isPlayersTurn : boolean) => {
            let heapSizeSum = 0;
            $.each(this.form.find('.heap-size'), (indexInArray, value) => {
                heapSizeSum += Number($(value).val());
            });
            if (heapSizeSum === 0) {
                this.log((isPlayersTurn ? 'Player' : 'Computer') + ' wins!');
                this.form
                    .find('.btn-play')
                    .val('Game Over');
                this.form
                    .find('.heap-form-group')
                    .hide();
                this.form
                    .find('.btn-hint')
                    .hide();
                return;
            }

            $.each(this.form.find('.heap-row'), (indexInArray, value) => {
                let heapRow = $(value);
                heapRow.find('.heap-buttons').empty();
                for (let i = Number(heapRow.find('.heap-size').val()); i > 0; i--) {
                    heapRow.find('.heap-buttons').append(
                        $('<input type="button" class="btn btn-primary btn-remove">')
                            .prop('value', i)
                            .attr('data-remove', i)
                    );
                }
                heapRow.find('.heap-form-group').show();
            });
        };

        // The computer's turn.
        computerMove = async () => {
            var nextMove = await this.getNextMove();

            // No move is returned for a losing position.
            // In that case, choose a random heap from those with at least one object left and choose a random number to remove.
            if (Object.keys(nextMove).length === 0) {
                let heaps = [];
                $.each(this.form.find('.heap-size'), (indexInArray, value) => {
                    heaps.push(Number($(value).val()));
                });
                let heapsInPlay = $.map(heaps, (elementOfArray, indexInArray) => {
                    return elementOfArray > 0 ? indexInArray: null;
                });
                let randomHeap = heapsInPlay[Math.floor(Math.random() * heapsInPlay.length)];
                let randomNumber = Math.floor(Math.random() * (heaps[randomHeap] - 1)) + 1;
                nextMove = {
                    heap: randomHeap,
                    number: randomNumber
                };
            }
            this.log('Computer removed ' + nextMove.number + ' from heap ' + (nextMove.heap + 1) + '.');
            let heapSize = this.form.find('.heap-row[data-heap-number=' + nextMove.heap + '] .heap-size');
            heapSize.val(Number(heapSize.val()) - nextMove.number);
            this.refreshButtons(true);
            this.form.find('.btn-remove').prop('disabled', false);
        };

        // Shows the player a hint.
        btnHintClick = async (e) => {
            $(e.currentTarget).blur();

            var nextMove = await this.getNextMove();
            let hint = this.form.find('.hint');

            if (Object.keys(nextMove).length === 0) {
                hint.val('Sorry, I can\'t help you.');
            }
            else {
                hint.val('Try removing ' + nextMove.number + ' from heap ' + (nextMove.heap + 1) + '.');
            }
            hint.fadeIn();
            setTimeout(() => hint.fadeOut(), 1000);
        };

        // Gets the next move from the solver.
        getNextMove = async () => {
            try {
                let heaps = [];
                $.each(this.form.find('.heap-size'), (indexInArray, value) => {
                    heaps.push(Number($(value).val()));
                });
                let data = JSON.stringify(heaps);
                let result = await $.ajax({
                    url: '/api/nextmove',
                    method: 'POST',
                    contentType: 'application/json',
                    data: data
                });
                return result;
            }
            catch (error) {
                console.log(error);
                this.log('There was an error.');
            }
        };
    }
}