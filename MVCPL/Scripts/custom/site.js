//TODO: refactor

function editFailed(result) {
    var errorsInfo = result;
    for (var i = 0; i < result.length; ++i) {
        var key = errorsInfo[i].key;
        var message = errorsInfo[i].errors[0];
        $("span[data-valmsg-for='" + key + "']").text(message);
    }
}

$('#formid').ajaxForm(function (data) {
    debugger;
    $("span[data-valmsg-for]").text('');
    if (typeof data === 'string') {
        $('#usersTableBody').append(data);
        $('#myModal').modal('hide');
    } else {
        editFailed(data);
    }
});

function editUser(userId) {
    $.ajax({
        url: 'user/' + userId + '/edit',
        success: function (data) {
            $('#modalWrapper').html(data);
            $('#editModal').modal();
        },
        error: function (error) {
            alert('Error!');
        }
    });
}

function rewardDetails(rewardId) {
    $.ajax({
        url: 'award/' + rewardId,
        success: function (data) {
            $('#rewardWrapper').html(data);
            $('#rewardDetailsModal').modal();
        },
        error: function (error) {
            alert('Error!');
        }
    });
}

jQuery.ajaxSetup({
    beforeSend: function () {
        $('#loaderImageContainer').show();
    },
    complete: function () {
        $('#loaderImageContainer').hide();
    },
    success: function () { }
});