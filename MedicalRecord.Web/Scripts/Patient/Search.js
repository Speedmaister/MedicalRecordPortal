$(function () {
    var comboBox = $("#patient-search-dropdown");
    var results = null;
    $.ajax({
        method: "POST",
        url: "/Patient/SearchOptions",
        data: null,
        success: function (data) {
            results = data;
            var displayValues = [];
            for (var i = 0; i < results.length; i++) {
                displayValues.push(results[i].Text);
            }

            $("#patient-search-dropdown").autocomplete({
                source: displayValues
            });
        }
    });

    $('#open-patient').click(function () {
        var egn = comboBox.val();
        var id = 0;
        for (var i = 0; i < results.length; i++) {
            if (results[i].Text == egn) {
                id = results[i].Value;
            }
        }

        if (id != 0) {
            window.location = "/Patient/GetById/" + id;
        }
    });
});