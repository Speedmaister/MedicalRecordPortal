(function () {
    var diseasesContainer = $('.diseases-container');
    var newDiseaseInput = $('#new-disease input');
    var medicalProceduresContainer = $('#medical-procedures-table tbody');
    var patientId = $('#patient-id');

    var newProcedure = {
        nameInput: $('#procedure-name'),
        dateInput: $('#procedure-date'),
        diagnoseInput: $('#procedure-diagnose'),
        toothNumberInput: $('#procedure-toothnumber'),
        priceInput: $('#procedure-price')
    }

    $(document).ready(function () {
        $('#add-procedure').click(function (e) {
            var newProcedureJson = {
                Name: newProcedure.nameInput.val(),
                Date: newProcedure.dateInput.val(),
                Diagnose: newProcedure.diagnoseInput.val(),
                ToothNumber: newProcedure.toothNumberInput.val(),
                Price: newProcedure.priceInput.val()
            }
            
                var newProcedureRow = document.createElement("tr");
                newProcedureRow.appendChild(createCell(newProcedureJson.Date));
                newProcedureRow.appendChild(createCell(newProcedureJson.Diagnose));
                newProcedureRow.appendChild(createCell(newProcedureJson.ToothNumber));
                newProcedureRow.appendChild(createCell(newProcedureJson.Name));
                newProcedureRow.appendChild(createCell(newProcedureJson.Price));
                medicalProceduresContainer.append(newProcedureRow);
        })

        $('#new-disease button').click(function () {
            var newDisease = newDiseaseInput.val();
            var actionType = "GET";
            var data = {
                newDisease: newDisease,
            };
            if (patientId.length != 0) {
                data.patientId = patientId.val();
                actionType = "POST";
            }

            $.ajax({
                method: actionType,
                data: data,
                url: "/Patient/Disease",
                success: function (diseaseTemplate) {
                    var disease = $(diseaseTemplate);
                    disease.children('img').click(removeDisease);
                    diseasesContainer.prepend(disease);
                    newDiseaseInput.val('Enter new disease');
                }
            })
        })

        $('.disease-entity img').click(function () {
            removeDisease(this);
        })

        $('#create-patient').click(function () {
            var patient = getPatienData();
            $.ajax({
                method: "POST",
                url: "/Patient/Index",
                data: {
                    patientJson: JSON.stringify(patient)
                },
                success: function (data) {
                    var x = 5;
                }
            });
        });


        $('#save-patient').click(function () {
            var patient = getPatienData();
            patient.Id = patientId.val();
            $.ajax({
                method: "POST",
                url: "/Patient/Save",
                data: {
                    patientJson: JSON.stringify(patient)
                },
                success: function (data) {
                    var x = 5;
                }
            });
        });
    })

    function getPatienData() {
        var patient = {
            FirstName: document.getElementById('FirstName').value,
            SurName: document.getElementById('SurName').value,
            LastName: document.getElementById('LastName').value,
            EGN: document.getElementById('EGN').value,
            Telephone: document.getElementById('Telephone').value,
            Address: document.getElementById('Address').value
        };

        patient.Diseases = [];
        var diseasesElements = $('.disease-entity');
        for (var i = 0; i < diseasesElements.length; i++) {
            var element = $(diseasesElements[i]);
            var disease = {
                Name: element.children('span')[0].innerText
            }

            if (patientId.length != 0) {
                disease.Id = element.attr('data-id');
            }

            patient.Diseases.push(disease);
        }

        patient.TeethStatus = {
            First: [],
            Second: [],
            Third: [],
            Fourth: [],
            Fifth: [],
            Sixth: [],
            Seventh: [],
            Eighth: []
        };

        var teethTable = $('.teeth-status-table tr');
        var activeTeeth = $('.tooth-number.enabled');
        for (var i = 0; i < activeTeeth.length; i++) {
            var toothNumber = activeTeeth[i].innerText;
            var statusRow = null;
            var quadrant = parseInt(toothNumber[0]);
            var orderNumber = parseInt(toothNumber[1]);
            if (quadrant == 1 || quadrant == 2 || quadrant == 5 || quadrant == 6) {
                statusRow = teethTable[1];
            }
            else {
                statusRow = teethTable[4];
            }

            var statusCell = null;
            var isLeftQuadrant = quadrant == 1 || quadrant == 4 || quadrant == 5 || quadrant == 8;
            if (isLeftQuadrant) {
                statusCell = $(statusRow).children('td')[8 - orderNumber];
            }
            else {
                statusCell = $(statusRow).children('td')[orderNumber - 1 + 8];
            }

            var toothModel = {
                Quadrant: quadrant,
                OrderNumber: orderNumber,
                IsActive: true,
                StateCode: $(statusCell).children('select').val()
            };

            switch (quadrant) {
                case 1:
                    patient.TeethStatus.First.push(toothModel);
                    break;
                case 2:
                    patient.TeethStatus.Second.push(toothModel);
                    break;
                case 3:
                    patient.TeethStatus.Third.push(toothModel);
                    break;
                case 4:
                    patient.TeethStatus.Fourth.push(toothModel);
                    break;
                case 5:
                    patient.TeethStatus.Fifth.push(toothModel);
                    break;
                case 6:
                    patient.TeethStatus.Sixth.push(toothModel);
                    break;
                case 7:
                    patient.TeethStatus.Seventh.push(toothModel);
                    break;
                case 8:
                    patient.TeethStatus.Eighth.push(toothModel);
                    break;
            }
        }

        var inactiveTeeth = $('.tooth-number.disabled');
        for (var i = 0; i < inactiveTeeth.length; i++) {
            var toothNumber = inactiveTeeth[i].innerText;
            var statusRow = null;
            var quadrant = parseInt(toothNumber[0]);
            var orderNumber = parseInt(toothNumber[1]);
            if (quadrant == 1 || quadrant == 2 || quadrant == 5 || quadrant == 6) {
                statusRow = teethTable[1];
            }
            else {
                statusRow = teethTable[4];
            }

            var statusCell = null;
            var isLeftQuadrant = quadrant == 1 || quadrant == 4 || quadrant == 5 || quadrant == 8;
            if (isLeftQuadrant) {
                statusCell = $(statusRow).children('td')[8 - orderNumber];
            }
            else {
                statusCell = $(statusRow).children('td')[orderNumber - 1 + 8];
            }

            var toothModel = {
                Quadrant: quadrant,
                OrderNumber: orderNumber,
                IsActive: false,
                StateCode: $(statusCell).children('select').val()
            };

            switch (quadrant) {
                case 1:
                    patient.TeethStatus.First.push(toothModel);
                    break;
                case 2:
                    patient.TeethStatus.Second.push(toothModel);
                    break;
                case 3:
                    patient.TeethStatus.Third.push(toothModel);
                    break;
                case 4:
                    patient.TeethStatus.Fourth.push(toothModel);
                    break;
                case 5:
                    patient.TeethStatus.Fifth.push(toothModel);
                    break;
                case 6:
                    patient.TeethStatus.Sixth.push(toothModel);
                    break;
                case 7:
                    patient.TeethStatus.Seventh.push(toothModel);
                    break;
                case 8:
                    patient.TeethStatus.Eighth.push(toothModel);
                    break;
            }
        }

        patient.Procedures = [];
        var proceduresRows = medicalProceduresContainer.children('tr');
        for (var i = 1; i < proceduresRows.length; i++) {
            var procedureFields = $(proceduresRows[i]).children('td');
            var procedure = {
                Date: procedureFields[0].innerText,
                Diagnose: procedureFields[1].innerText,
                ToothNumber: procedureFields[2].innerText,
                Name: procedureFields[3].innerText,
                Price: procedureFields[4].innerText,
                Id: $(procedureFields[5]).children('input[type=hidden]').value
            };

            patient.Procedures.push(procedure);
        }

        return patient;
    }

    function createCell(value) {
        var cell = document.createElement("td");
        cell.innerText = value;
        return cell;
    }

    function removeDisease(imageElement) {
        var diseaseId = this.parentNode.getAttribute("data-id");
        if (patientId.length == 0) {
            $(imageElement.currentTarget.parentNode).remove();
        }
        else {
            $.ajax({
                method: "POST",
                url: "/Patient/RemoveDisease",
                data: {
                    diseaseId: disease,
                    patientId: patientId.val()
                },
                success: function () {
                    $(imageElement.currentTarget.parentNode).remove();
                }
            })
        }
    }
}())