class Cloth {

    initializeView() {
        this.AddClothModal = $('#AddClothModal');
        this.addFormCloth = this.AddClothModal.find('#addFormCloth');
        this.name = this.addFormCloth.find('#name');
        this.AddClothBtn = this.addFormCloth.find('.add-cloth');
        return this;
    }
    
    bindEvents() {

        this.AddClothBtn.click((event) => {
            const url = '/Cloth/SetCloth'
            this.AddForm(url);
            return false;

        });

        $(document).on("click", ".delete-cloth", (event) => {
            event.preventDefault();
            const removeCard = $(event.target).closest("tr");
            this.deleteCloth(removeCard);
            return false;
        });

        $(document).on('click', '.edit-cloth', (e) => {
            this.selectedCard = $(e.target).closest("tr");
            const Id = this.selectedCard.data('id');
            const Name = this.selectedCard.data('name');
            console.log(Id);
            $('#Id').val(Id);
            this.name.val(Name);
            this.AddClothModal.modal('show');
        })

        $(document).on('click', '.add-cloth', (e) => {
            this.addFormCloth.get(0).reset();
            this.AddClothModal.modal('show');
        });
    }


    deleteCloth(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });
        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد حذف القماش ؟",
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
                    'لقد تم حذف القماش',
                    'success'
                )
            }
        });
    }



    submitDeleteColor(removedCard) {
        const removedClothId = removedCard.data("id");
        $.ajax({
            url: `/Cloth/RemoveCloth/${removedClothId}`,
            type: "Delete",
            dataType: "json"
        }).done((json) => {
            let ClothTable = $('#ClothTable').DataTable();
            ClothTable.row($('.id-' + removedClothId)).remove().draw(false);
        }).fail((xhr, status, errorThrown) => {

        }).always((xhr, status) => {

        });
    }


    AddForm(url) {

        if (this.name.val().trim() === "") {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى كتابة اسم القماش"
            });
            return false;
        }

      


        var formCloth = new FormData(this.addFormCloth[0]);
        $.ajax({
            url: url,
            data: formCloth,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) => {

            if (json === null) {
                Swal.fire({
                    icon: "warning",
                    timer: 3000,
                    title: "تنبيه",
                    text: "لا يمكنك إضافة هذا القماش فهو موجود مسبقا"
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
                this.addFormCloth.get(0).reset();
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

                this.selectedCard.find('.name').html(json.name);
            }
        })
    }

    AddRow(json) {
        console.log(json);
        console.log(json.id);
        var ClothTable = $('#ClothTable').DataTable();
        ClothTable.row.add($(` <tr data-id="${json.id}" data-name="${json.name}"
									  role="row" class="odd id-${json.id}">
                                    <td class="name">${json.name}</td>
                                    <td><a class="feather icon-edit-1 edit-cloth"></a></td>
                                    <td><a class="btn trash  delete-cloth" style="color:red" ><i class="fa fa-remove"></i></a></td>
                                 </tr> `)).draw(false);
    }  
}

$(document).ready(() => {
    new Cloth()
        .initializeView()
        .bindEvents();
})