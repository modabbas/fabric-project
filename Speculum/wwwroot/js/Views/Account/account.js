class Accounts {

    // For Intialize The Proberties 
    initializeView() {
        this.accountForm = $('#accountForm');
        this.editFormUser = $('#EditAccountForm');
        this.SaveEdit = $('#SaveEdit');
        this.AdminUser = $('#count');
        return this;
    }

    // Take Events For Example Click on
    bindEvents() {
        this.accountForm.on("submit", (e) => {
            e.preventDefault();
            this.submitAddUser(); // Create New Accout 
            return false;
        });

        $(document).on("click", ".delete-user", (event) => {
            event.preventDefault();
            const removedCard = $(event.target).closest("tr"); // Delete Account from DB

            this.deleteUser(removedCard);
            return false;
        });

        $(document).on('click', '#updUser', (e) => {
            this.editRow = $(e.target).closest("tr");
            this.fillEditUserModal();
            return false;
        });

        this.SaveEdit.click((event) => {
            event.preventDefault();// Edit Accout of User
            this.submitEditForm();
            return false;
        });

    }


    submitEditForm() {
        if ($('#username').val().trim().length < 4) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه ",
                text: "الرجاء اختيار اسم مستخدم من أربعة رموز وأكثر"
            });
            return false;
        }

        if ($('#password').val().trim().length < 4) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه ",
                text: "الرجاء اختيار كلمة سر من أربعة رموز وأكثر"
            });
            return false;
        }
        loadingPage({
            action: 'add'
        });
        var form = new FormData(this.editFormUser[0]);
        $.ajax({
            url: "/Account/UpdateUser",
            data: form,
            type: "PUT",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) => {
            loadingPage({
                action: 'remove'
            });
            if (json.testing === true)
            {
                this.actionUserRow(json);
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'تم تعديل المستخدم الجديد بنجاح ',
                    showConfirmButton: false,
                    timer: 2000
                });
                $('#EditAccountModal').modal('hide');
            }

            else {
                // Access Denied
                switch (json) {
                    case "InvalidUserName":
                        Swal.fire({
                            icon: "warning",
                            timer: 3000,
                            title: "تنبيه ",
                            text: "اسم المستخدم غير صالح"
                        });
                        return false;
                    case "DuplicateUserName":
                        Swal.fire({
                            icon: "warning",
                            timer: 3000,
                            title: "تنبيه ",
                            text: "اسم المستخدم موجود مسبقاً"
                        });
                        return false;
                }
            }

        });

    }



    deleteUser(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });

        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد حذف هذا المستخدم ؟",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'نعم!',
            cancelButtonText: 'إلغاء',
            reverseButtons: false

        }).then((result) => {
            if (result.value) {
                this.submitDeleteUser(removedCard);
                swalWithBootstrapButtons.fire(
                    'تم!',
                    'لقد تم حذف المستخدم',
                    'success'
                )
            }
        });
    }

    submitDeleteUser(removedCard) {
        const removedUserId = removedCard.data("id");
        loadingPage({
            action: 'add'
        });
        $.ajax({
            url: `/Account/RemoveUser/${removedUserId}`,
            type: "Delete",
            dataType: "json"
        }).done((json) => {
            loadingPage({
                action: 'remove'
            });
            let table = $('#UserTable').DataTable();
            table.row($('.id-' + removedUserId)).remove().draw(false);
        }).fail((xhr, status, errorThrown) => {

        }).always((xhr, status) => {

        });
    }


    actionUserRow(json) {
        this.editRow.data("username", json.userName);
        this.editRow.data("role", json.role);
        this.editRow.find(".name").html(json.userName);
        switch (json.role)
        {
            case 'Admin':
                this.editRow.find(".role").html('مدير عام');
                break
            case 'Receiving':
                this.editRow.find(".role").html('أمين مستودع الاستلام');
                break;
            case 'Delivery':
                this.editRow.find(".role").html('مسؤول التسليم');
                break;
            case 'Machine':
                this.editRow.find(".role").html('مسؤول الماكينات');
                break;
            case 'Laboratory':
                this.editRow.find(".role").html('مسؤول المخبر');
                break;
        }
    }

    fillEditUserModal()
    {
        const row = this.editRow.data("id");
        const username = this.editRow.data("username");
        const role = this.editRow.data('role');

        $('#username').val(username);
        $('#userId').val(row);
        $('#password').val('●●●●●●●●');
        $('#EditRoleSelect').val(role);
    }

    submitAddUser() {

        if ($('#roleSelect').val() === null) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه ",
                text: "يرجى اختيار صلاحية للمستخدم"
            });
            return false;
        }


        if ($('#username').val().trim().length < 4) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه ",
                text: "لا يمكن كتابة أقل من أربع أحرف لاسم المستخدم"
            });
            return false;
        }

       
        if ($('#password').val().trim().length < 4) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه ",
                text: "لا يمكن كتابة أقل من أربع أحرف لكلمة السر"
            });
            return false;
        }
        $('#createUser').hide(); // Hide The Button of CreateUser
        loadingPage({
            action: 'add'
        });
        var form = new FormData(this.accountForm[0]);
        $.ajax({
            url: "/Account/Register",
            data: form,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) => {
            loadingPage({
                action: 'remove'
            });
            $('#createUser').show(); // Show The Button of CreateUser
            if (json === null)
            {
                Swal.fire({
                    icon: "warning",
                    timer: 3000,
                    title: "تنبيه ",
                    text: "خطأ في كتابة اسم المستخدم او كلمة السر"
                });
                return false;
            }
            else if (json === true)
            {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'تم إضافة مستخدم جديد بنجاح ',
                    showConfirmButton: false,
                    timer: 2000
                });
                $('#username').val('');
                $('#email').val('');
                $('#password').val('');
                $("#roleSelect").prop("selectedIndex", 0);
            }
            else {
                switch (json)
                {
                    case "InvalidUserName":
                        Swal.fire({
                            icon: "warning",
                            title: "تنبيه ",
                            timer: 3000,
                            text: "اسم المستخدم غير صالح"
                        });
                        return false;
                    case "DuplicateUserName":
                        Swal.fire({
                            icon: "warning",
                            title: "تنبيه ",
                            timer: 3000,
                            text: "اسم المستخدم موجود مسبقاً"
                        });
                        return false;
                }
            }

        });
    }
}


$(document).ready(() => {
    new Accounts()
        .initializeView()
        .bindEvents();
});