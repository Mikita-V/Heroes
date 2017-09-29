//TODO: refactor

$.ajaxSetup({
    beforeSend: function () {
        $('#loaderImageContainer').show();
    },
    complete: function () {
        $('#loaderImageContainer').hide();
    }
});

$('#createUserForm').ajaxForm(function (data) {
    createUser(data, '#usersTableBody');
});

$('#createUserSafeForm').ajaxForm(function (data) {
    createUser(data, '#sessionInfoModalWrapper');
});

$('#sessionInfoMenuItem a').click(function() {
    $('#sessionInfoModal').modal();
});

$('#btnCreateUser').click(function (result) {
    ajaxValidate(result);
});

$('#btnCreateUserSafe').click(function (result) {
    ajaxValidate(result);
    $('#sessionInfoMenuItem').show();
});

function createUser(data, userTableId) {
    clearErrorMessages();
    if (typeof data === 'string') {
        $(userTableId).html(data);
        $('#createUserModal').modal('hide');
    } else {
        ajaxValidate(data);
    }
}

function ajaxValidate(result) {
    var errorsInfo = result;
    if (errorsInfo != null && errorsInfo.length !== 0) {
        for (var i = 0; i < result.length; ++i) {
            var key = errorsInfo[i].key;
            var message = errorsInfo[i].errors[0];
            $("span[data-valmsg-for='" + key + "']").text(message);
        }
    }
}

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

function showSessionMenuItem() {
    $('#sessionInfoMenuItem').show();
}

function clearErrorMessages() {
    $("span[data-valmsg-for]").text('');
}