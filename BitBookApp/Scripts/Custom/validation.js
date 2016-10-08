
$("#registration").validate({
    rules: {
        Email: {
            required: true,
            email:true
        }


    },
    messages: {
        Email: {
            required: " <i>Enter a email please </i> <br/>",
            email:"enter corrent email formet"
        }
    }
});
