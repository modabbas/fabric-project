class Lab {

    initializeView() {

        this.AddLabModal = $('#AddLabModal');
        this.AddLabForm = this.AddLabModal.find('#AddLabForm');
        this.ColorSelect = this.AddLabForm.find('#ColorSelect');
        this.ClothSelect = this.AddLabForm.find('#ClothSelect');
        this.percent = this.AddLabForm.find('#percent');
        this.AddLab = this.AddLabForm.find('.add-sample');
        this.EditLabModal = $('#EditLabModal');
        this.EditLabForm = this.EditLabModal.find('#EditLabForm');
        this.EditColorSelect = this.EditLabForm.find('#EditColorSelect');
        this.EditClothSelect = this.EditLabForm.find('#EditClothSelect');
        this.EditPercent = this.EditLabForm.find('#EditPercent');
        this.EditBtn = this.EditLabForm.find('.editBtn');


        return this;
    }

    bindEvents() {

        this.EditBtn.click((event) => {
            const url = '/Lab/EditSample'
            this.EditForm(url);
            return false;

        });


        this.AddLab.click((event) => {
            const url = '/Lab/SetSample'
            this.AddForm(url);
            return false;

        });

        $(document).on("click", ".delete-sample", (event) => {
            event.preventDefault();
            const removeCard = $(event.target).closest("tr");
            this.deletesample(removeCard);
            return false;
        });

        $(document).on('click', '.edit-sample', (e) => {
            this.selectedCard = $(e.target).closest("tr");
            const Id = this.selectedCard.data('id');
            console.log(Id);
            const ClothName = this.selectedCard.data('clothid');
            const ColorName = this.selectedCard.data('colorid');
            const Percent = this.selectedCard.data('percent');
            this.EditClothSelect.val(ClothName);
            this.EditColorSelect.val(ColorName);
            this.EditPercent.val(Percent);
            $('#Id').val(Id);
            this.EditLabModal.modal('show');
        })

        $(document).on('click', '.add-sample', (e) => {
            this.addFormLab.get(0).reset();
            this.AddLabModal.modal('show');
        });
    }


    deletesample(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });
        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد حذف هذه التجربة المخبرية ؟",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'نعم!',
            cancelButtonText: 'إلغاء',
            reverseButtons: false

        }).then((result) => {
            if (result.value) {
                this.submitDeleteSample(removedCard);
                swalWithBootstrapButtons.fire(
                    'تم!',
                    'تم حذف التجربة المخبرية',
                    'success'
                )
            }
        });
    }



    submitDeleteSample(removedCard) {
        const removedSampleId = removedCard.data("id");
        $.ajax({
            url: `/Lab/RemoveSample/${removedSampleId}`,
            type: "Delete",
            dataType: "json"
        }).done((json) => {
            let LabTable = $('#LabTable').DataTable();
            LabTable.row($('.id-' + removedSampleId)).remove().draw(false);
        }).fail((xhr, status, errorThrown) => {

        }).always((xhr, status) => {

        });
    }


    EditForm(url) {
        if (this.EditClothSelect.val() === null)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى اختيار نوعية القماش المناسبة لعملية الإختبار "
            });
            return false;
        }

        if (this.EditColorSelect.val() === null)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى اختيار اللون المناسب لعملية الإختبار "
            });
            return false;
        }
        if (this.EditPercent.val() === "" ||
            parseFloat(this.EditPercent.val()) === 0 ||
            parseFloat(this.EditPercent.val()) < 0) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة النسبة"
            });
            return false;
        }


        var formlab = new FormData(this.EditLabForm[0]);
        $.ajax({
            url: url,
            data: formlab,
            type: "PUT",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) => {
            $('.close').click();
            $('body').css({
                padding: 0
            })
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'تمت عملية التعديل بنجاح',
                showConfirmButton: false,
                timer: 3000,
            });
            this.selectedCard.data('clothid', json.clothId);
            this.selectedCard.data('colorid', json.newColorId);
            this.selectedCard.data('percent', json.succuessPercent);

            this.selectedCard.find('.ColthName').html(json.clothName);
            this.selectedCard.find('.ColorName').html(json.colorName);
            this.selectedCard.find('.Percent').html(json.succuessPercent);
        })
    }

    AddForm(url) {
        if (this.ClothSelect.val() === null)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى اختيار نوعية القماش المناسبة "
            });
            return false;
        }

        if (this.ColorSelect.val() === null) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى اختيار اللون المناسب لعملية الإختبار "
            });
            return false;
        }
       
        if (parseFloat(this.percent.val()) === 0 ||
            parseFloat(this.percent.val()) < 0 ||
            parseFloat(this.percent.val()) > 100) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة نسبة النجاح"
            });
            return false;
        }


        var formLab = new FormData(this.AddLabForm[0]);
        $.ajax({
            url: url,
            data: formLab,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) => {
            $('.close').click();
            $('body').css({
                padding: 0
            })
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'تم إضافة عينة جديدة بنجاح ',
                showConfirmButton: false,
                timer: 3000,
            });
            this.AddRow(json);
            this.AddLabForm.get(0).reset();
        })
    }

    AddRow(json) {
        var LabTable = $('#LabTable').DataTable();
        LabTable.row.add($(`<tr data-id="${json.id}" data-percent="${json.succuessPercent}"
                                                    data-clothid="${json.clothId}"
                                                    data-colorid="${json.newColorId}"  
                                                    role="row" class="odd id-${json.id}">
                                                    <td>${json.id}</td>
                                                    <td class="ClothName">${json.clothName}</td>
                                                    <td class="ColorName">${json.colorName}</td>
                                                    <td class="Percent">${json.succuessPercent}</td>
                                                    <td><a class="feather icon-edit-1 edit-sample"></a></td>
                                                    <td><a class="btn trash delete-sample" style="color:red"><i class="fa fa-remove"></i></a></td>
                                                </tr> `)).draw(false);
    }
}

$(document).ready(() => {
    new Lab()
        .initializeView()
        .bindEvents();
})