(function () {
    var toothStateCells = $('.tooth-state');
    var diseasesContainer = $('.diseases-container');
    var newDiseaseInput = $('#new-disease input');

    var newProcedure = {
        nameInput: $('#procedure-name'),
        dateInput: $('#procedure-date'),
        diagnoseInput: $('#procedure-diagnose'),
        toothIdInput: $('#procedure-toothid'),
        priceInput: $('#procedure-price')
    }

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
        $('#add-procedure').click(function (e) {
            var newProcedureJson = {
                Name: newProcedure.nameInput.val(),
                Date: newProcedure.dateInput.val(),
                Diagnose: newProcedure.diagnoseInput.val(),
                ToothId: newProcedure.toothIdInput.val(),
                Price: newProcedure.priceInput.val()
            }
        })

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

        $.each(toothNumbers, function (i, item) {
            newProcedure.toothIdInput.append($('<option>', {
                value: item,
                text: item
            }))
        })

        $('#new-disease button').click(function () {
            var newDisease = newDiseaseInput.val();
            $.ajax({
                method: "GET",
                url: "Patient/Disease",
                success: function (diseaseTemplate) {
                    var disease = $(diseaseTemplate);
                    disease.children('span').html(newDisease);
                    disease.children('img').click(removeDisease);
                    diseasesContainer.prepend(disease);
                    newDiseaseInput.val('Enter new disease');
                }
            })
        })

        $('.disease-entity img').click(function () {
            removeDisease(this);
        })
    })

    function removeDisease(imageElement) {
        var disease = this.previousSibling.previousSibling.innerText;
        $.ajax({
            method: "POST",
            url: "Patient/RemoveDisease",
            data:disease,
            success: function () {
                $(imageElement.currentTarget.parentNode).remove();
            }
        })
    }
}())