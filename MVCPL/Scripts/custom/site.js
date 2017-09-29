//TODO: refactor

$.ajaxSetup({
    beforeSend: function () {
        $('#loaderImageContainer').show();
    },
    complete: function () {
        $('#loaderImageContainer').hide();
    }
});

function ajaxValidate(result) {
    var errorsInfo = result;
    for (var i = 0; i < result.length; ++i) {
        var key = errorsInfo[i].key;
        var message = errorsInfo[i].errors[0];
        $("span[data-valmsg-for='" + key + "']").text(message);
    }
}

$('#createUserForm').ajaxForm(function (data) {
    $("span[data-valmsg-for]").text('');
    if (typeof data === 'string') {
        $('#usersTableBody').append(data);
        $('#createUserModal').modal('hide');
    } else {
        ajaxValidate(data);
    }
});

$('#createUserSafeForm').ajaxForm(function (data) {
    $("span[data-valmsg-for]").text('');
    if (typeof data === 'string') {
        $('#sessionInfo').html(data);
        $('#createUserModal').modal('hide');
    } else {
        ajaxValidate(data);
    }
});

function editUser(userId) {
    $.ajax({
        url: 'user/' + userId + '/edit',
        success: function (data) {
            $('#editUserModalWrapper').html(data);
            $('#editUserModal').modal();
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
            $('#rewardDetailsModalWrapper').html(data);
            $('#rewardDetailsModal').modal();
        },
        error: function (error) {
            alert('Error!');
        }
    });
}