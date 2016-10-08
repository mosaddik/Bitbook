$(document)
    .ready(function () {
       
        //hide
        var hidden = $("#hiddenToUserAtLoad");
        hidden.hide();
        //show
        $("#ViewModelRegistration_Email")
            .click(function() {
                hidden.show();
            });



    });
  