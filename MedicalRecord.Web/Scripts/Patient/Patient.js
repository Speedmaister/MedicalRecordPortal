(function () {
    var diseasesContainer = $('.diseases-container');
    var newDiseaseInput = $('#new-disease input');
    var patientId = $('#patient-id');

    var newProcedure = {
        nameInput: $('#procedure-name'),
        dateInput: $('#procedure-date'),
        diagnoseInput: $('#procedure-diagnose'),
        toothIdInput: $('#procedure-toothid'),
        priceInput: $('#procedure-price')
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

        $('#new-disease button').click(function () {
            var newDisease = newDiseaseInput.val();
            $.ajax({
                method: "POST",
                data:newDisease,
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

        $('#save-patient').click(function () {

        });
    })

    function removeDisease(imageElement) {
        var disease = this.previousSibling.previousSibling.innerText;
        if (patientId.length == 0) {
            $(imageElement.currentTarget.parentNode).remove();
        }
        else {
            $.ajax({
                method: "POST",
                url: "Patient/RemoveDisease",
                data: {
                    disease: disease,
                    patientId: patientId.val()
                },
                success: function () {
                    $(imageElement.currentTarget.parentNode).remove();
                }
            })
        }
    }
}())