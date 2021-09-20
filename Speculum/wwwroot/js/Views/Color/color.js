class Color {

    initializeView() {
        this.AddColorModal = $('#AddColorModal');
        this.addFormColor = this.AddColorModal.find('#addFormColor');
        this.name = this.addFormColor.find('#name');
        this.amount = this.addFormColor.find('#amount');
        this.AddColorBtn = this.addFormColor.find('.add-color');
        return this;
    }

    bindEvents() {

        this.AddColorBtn.click((event) => {
            const url = '/Color/SetColor'
            this.AddForm(url);
            return false;

        });

        $(document).on("click", ".delete-color", (event) => {
            event.preventDefault();
            const removeCard = $(event.target).closest("tr");
            this.deleteColor(removeCard);
            return false;
        });

        $(document).on('click', '.edit-color', (e) => {
            this.selectedCard = $(e.target).closest("tr");
            const Id = this.selectedCard.data('id');
            const Name = this.selectedCard.data('name');
            const Amount = this.selectedCard.data('amount');
            console.log(Id);
            $('#Id').val(Id);
            this.name.val(Name);
            this.amount.val(Amount);
            this.AddColorModal.modal('show');
        })

        $(document).on('click', '.add-color', (e) => {
            this.addFormColor.get(0).reset();
            this.AddColorModal.modal('show');
        });
    }


    deleteColor(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });
        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد حذف اللون ؟",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'نعم!',
            cancelButtonText: 'إلغاء',
            reverseButtons: false

        }).then((result) => {
            if (result.value) {
                this.submitDeleteColor(removedCard);
                swalWithBootstrapButtons.fire(
                    'تم!',
                    'لقد تم حذف اللون',
                    'success'
                )
            }
        });
    }



    submitDeleteColor(removedCard) {
        const removedColorId = removedCard.data("id");
        $.ajax({
            url: `/Color/RemoveColor/${removedColorId}`,
            type: "Delete",
            dataType: "json"
        }).done((json) => {
            let ColorTable = $('#ColorTable').DataTable();
            ColorTable.row($('.id-' + removedColorId)).remove().draw(false);
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
                title: "تنبيه",
                text: "يرجى كتابة اسم اللون"
            });
            return false;
        }

        if (this.amount.val().trim() === "") {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى اختيار الكمية الخاصة باللون"
            });
            return false;
        }

        if (parseFloat(this.amount.val()) < 0 || parseFloat(this.amount.val()) === 0)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "لا يمكن أن تكون الكمية صفر أو سالبة"
            });
            return false;
        }


        var formColor = new FormData(this.addFormColor[0]);
        $.ajax({
            url: url,
            data: formColor,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) => {

            if (json === null)
            {
                Swal.fire({
                    icon: "warning",
                    timer: 3000,
                    title: "تنبيه",
                    text: "لا يمكنك إضافة هذا اللون فهو موجود مسبقا"
                });
                return false;
            }
            $('.close').click();
            $('body').css({
                padding: 0
            })

            if (json.isAdd === true)
            {  // Add Form
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'تم إضافة لون جديد بنجاح ',
                    showConfirmButton: false,
                    timer: 3000,
                });
                this.AddRow(json);
                this.addFormColor.get(0).reset();
            }
            else  // Edit Form
            {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'تمت عملية التعديل بنجاح',
                    showConfirmButton: false,
                    timer: 3000,
                });
                this.selectedCard.data('name', json.name);
                this.selectedCard.data('amount', json.amount);

                this.selectedCard.find('.name').html(json.name);
                this.selectedCard.find('.amount').html(json.amount);
            }

        })
    }

    AddRow(json) {
        console.log(json);
        console.log(json.id);
        var ColorTable = $('#ColorTable').DataTable();
        ColorTable.row.add($(` <tr data-id="${json.id}" data-name="${json.name}"
									  data-amount="${json.amount}" 
									  role="row" class="odd id-${json.id}">
                                    <td class="name">${json.name}</td>
                                    <td class="amount">${json.amount}</td>
                                    <td><a class="feather icon-edit-1 edit-color"></a></td>
                                    <td><a class="btn trash  delete-color" style="color:red" ><i class="fa fa-remove"></i></a></td>
                                 </tr> `)).draw(false);
    }
}

$(document).ready(() => {
    new Color()
        .initializeView()
        .bindEvents();
})