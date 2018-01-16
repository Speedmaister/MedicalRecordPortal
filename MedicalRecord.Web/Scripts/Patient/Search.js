$(function () {
    var comboBox = $("#patient-search-dropdown");
    var results = [];
    $.ajax({
        method: "POST",
        url: "/Patient/SearchOptions",
        data: null,
        success: function (data) {
            var displayValues = [];
            for (var i = 0; i < data.length; i++) {
                var displayValue = data[i].FirstName + ' ' + data[i].SurName + ' ' + data[i].LastName + ' ' + data[i].EGN;
                displayValues.push(displayValue);
                results.push({
                    text: displayValue,
                    value: data[i]
                });
            }

            $("#patient-search-dropdown").autocomplete({
                source: displayValues
            });
        }
    });

    $('#open-patient').click(function () {
        var selectedValue = comboBox.val();
        var id = 0;
        for (var i = 0; i < results.length; i++) {
            if (results[i].text == selectedValue) {
                id = results[i].value.Value;
            }
        }

        if (id != 0) {
            window.location = "/Patient/GetById/" + id;
        }
    });
});