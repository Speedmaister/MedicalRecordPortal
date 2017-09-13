(function () {
    var teeth = $('.tooth-number');
    var toothStateCells = $('.tooth-state');

    var toothNumbers = [];
    for (var i = 1; i <= 4; i++) {
        for (var j = 1; j <= 8; j++) {
            var toothNumber = i * 10 + j;
            toothNumbers.push(toothNumber);
        }
    }

    for (var i = 5; i <= 8; i++) {
        for (var j = 0; j <= 5; j++) {
            var toothNumber = i * 10 + j;
            toothNumbers.push(toothNumber);
        }
    }

    $(document).ready(function () {

        var dropdownTemplate;
        $.ajax({
            method: "GET",
            url: "Patient/Tooth",
            success: function (htmlTemplate) {
                dropdownTemplate = htmlTemplate;
                for (var i = 0; i < toothStateCells.length; i++) {
                    var cell = $(toothStateCells[i]);
                    var value = cell.text();
                    cell.html(dropdownTemplate);
                    cell.val(value);
                }
            }
        })

        var toothIdInput = $('#procedure-toothid');
        $.each(toothNumbers, function (i, item) {
            toothIdInput.append($('<option>', {
                value: item,
                text: item
            }))
        })

        teeth.click(function () {
            var selectedTooth = $(this);
            var isDisabled = selectedTooth.hasClass('disabled');
            if (isDisabled) {
                var toothNumber = selectedTooth.text();
                if (toothNumber.length != 2) {
                    alert('Issue with page content! Please refresh page.');
                    return;
                }

                var quadrant = toothNumber.substr(0, 1);
                var number = toothNumber.substr(1, 1);
                var oppositeTooth = getOppositeTooth(quadrant, number);
                var oppositeToothElement;
                if (oppositeTooth) {
                    oppositeToothElement = getToothElementByNumber(oppositeTooth);
                }

                if (oppositeToothElement.length == 1) {
                    oppositeToothElement.addClass('disabled').removeClass('enabled');
                    selectedTooth.addClass('enabled').removeClass('disabled');
                }
            }
        })
    });

    function getOppositeTooth(quadrant, number) {
        var oppositeQuadrant = 0;
        switch (parseInt(quadrant)) {
            case 1:
                oppositeQuadrant = 5;
                break;
            case 2:
                oppositeQuadrant = 6;
                break;
            case 3:
                oppositeQuadrant = 7;
                break;
            case 4:
                oppositeQuadrant = 8;
                break;
            case 5:
                oppositeQuadrant = 1;
                break;
            case 6:
                oppositeQuadrant = 2;
                break;
            case 7:
                oppositeQuadrant = 3;
                break;
            case 8:
                oppositeQuadrant = 4;
                break;
            default:
                alert('Issue with page content! Please refresh page.');
                return;
        }

        var oppositeTooth = 0;
        var toothNumber = parseInt(number);
        if (0 < toothNumber && toothNumber < 6) {
            oppositeTooth = toothNumber;
        }

        if (oppositeQuadrant == 0 || oppositeTooth == 0) {
            return false;
        }

        return oppositeQuadrant.toString() + oppositeTooth.toString();
    }

    function getToothElementByNumber(number) {
        var toothElement;
        for (var i = 0; i < teeth.length; i++) {
            var item = teeth[i];
            if (item.innerText == number) {
                toothElement = $(item);
                break;
            }
        }

        return toothElement;
    }
}())