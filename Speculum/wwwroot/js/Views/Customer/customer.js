class Customer {

    initializeView() {
        this.AddCustomerModal = $('#AddCustomerModal');
        this.addFormCustomer = this.AddCustomerModal.find('#addFormCustomer');
        this.name = this.addFormCustomer.find('#name');
        this.phone = this.addFormCustomer.find('#phone');
        this.email = this.addFormCustomer.find('#email');
        this.AddCustomerBtn = this.addFormCustomer.find('.add-customer');
        return this;
    }

    bindEvents() {
        this.AddCustomerBtn.click((event) => {
            event.preventDefault();
            const url = '/Customer/SetCustomer'
            this.AddForm(url);
            return false;
        });

        $(document).on("click", ".delete-customer", (event) => {
            event.preventDefault();
            const removeCard = $(event.target).closest("tr");
            this.deleteCustomer(removeCard);
            return false;
        });

        $(document).on('click', '.edit-customer', (e) => {
            this.selectedCard = $(e.target).closest("tr");
            const Id = this.selectedCard.data('id');
            const Name = this.selectedCard.data('name');
            const Phone = this.selectedCard.data('phone');
            const Email = this.selectedCard.data('email');
            console.log(Id);
            $('#CustomerId').val(Id);
            this.name.val(Name);
            this.phone.val(Phone);
            this.email.val(Email);
            this.AddCustomerModal.modal('show');
        })

        $(document).on('click', '.add-customer', (e) => {
            this.addFormCustomer.get(0).reset();
            this.AddCustomerModal.modal('show');
        })
    }

    deleteCustomer(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });
        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد حذف الزبون ؟",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'نعم!',
            cancelButtonText: 'إلغاء',
            reverseButtons: false

        }).then((result) => {
            if (result.value) {
                this.submitDeleteCustomer(removedCard);
                swalWithBootstrapButtons.fire(
                    'تم!',
                    'لقد تم حذف الزبون',
                    'success'
                )
            }
        });
    }


    submitDeleteCustomer(removedCard) {
        const removedCustomerId = removedCard.data("id");
        $.ajax({
            url: `/Customer/CustomerRemove/${removedCustomerId}`,
            type: "Delete",
            dataType: "json"
        }).done((json) => {
            let CustomerTable = $('#CustomerTable').DataTable();
            CustomerTable.row($('.id-' + removedCustomerId)).remove().draw(false);
        }).fail((xhr, status, errorThrown) => {

        }).always((xhr, status) => {

        });
    }


    AddForm(url) {

        if (this.name.val().trim() === "")
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه ",
                text: "الرجاء اختيار اسم للزبون"
            });
            return false;
        }

        if (this.phone.val().trim() === "") {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه ",
                text: "الرجاء اختيار رقم هاتف للزبون"
            });
            return false;
        }


        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        var email = this.email.val().trim();
        if (!emailReg.test(email)) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "خطأ في كتابة الإيميل"
            });
            return false;
        }
        var formCustomer = new FormData(this.addFormCustomer[0]);
        $.ajax({
            url: url,
            data: formCustomer,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) => {
            $('.close').click();
            $('body').css({
                padding: 0
            })

            if (json.isAdd === true)
            {  // Add Form
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'تم إضافة زبون جديد بنجاح ',
                    showConfirmButton: false,
                    timer: 3000,
                });
                this.AddRow(json);
                this.addFormCustomer.get(0).reset();
            }
            else  // Edit Form
            {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'تم تعديل معلومات الزبون بنجاح',
                    showConfirmButton: false,
                    timer: 3000,
                });
                this.selectedCard.data('name', json.name);
                this.selectedCard.data('phone', json.phone);
                this.selectedCard.data('email', json.email);

                this.selectedCard.find('.name').html(json.name);
                this.selectedCard.find('.phone').html(json.phone);
                this.selectedCard.find('.email').html(json.email);
            }
           
        })
    }

    AddRow(json) {

        if (json.email === null)
        {
            json.email = "لا يوجد";
        }
        var CustomerTable = $('#CustomerTable').DataTable();
        CustomerTable.row.add($(` <tr data-id="${json.customerId}" data-name="${json.name}"
									  data-phone="${json.phone}" data-email="${json.email}"
									  role="row" class="odd id-${json.customerId}">
                                    <td>${json.customerId}</td>
                                    <td class="name">${json.name}</td>
                                    <td class="phone">${json.phone}</td>
                                    <td class="email">${json.email}</td>
                                    <td><a class="feather icon-edit-1 edit-customer"></a></td>
                                    <td><a class="btn trash delete-customer" style="color:red" ><i class="fa fa-remove"></i></a></td>
                                 </tr> `)).draw(false);
    }
}

$(document).ready(() => {
    new Customer()
        .initializeView()
        .bindEvents();
})