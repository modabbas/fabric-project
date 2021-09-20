class bigOrder {

    initializeView() {

        this.AddOrderModal = $('#AddOrderModal');
        this.AddOrderForm = this.AddOrderModal.find('#AddOrderForm');
        this.CustomerSelect = this.AddOrderForm.find('#CustomerSelect');
        this.TypeFabricSelect = this.AddOrderForm.find('#TypeFabricSelect');
        this.FabricWeight = this.AddOrderForm.find('#FabricWeight');
        this.cotton = this.AddOrderForm.find('#cotton');
        this.polister = this.AddOrderForm.find('#polister');
        this.AddBigOrder = this.AddOrderForm.find('.add-big-order');


        this.EditOrderModal = $('#EditOrderModal');
        this.EditOrderForm = this.EditOrderModal.find('#EditOrderForm');
        this.EditCustomerSelect = this.EditOrderForm.find('#EditCustomerSelect');
        this.EditClothSelect = this.EditOrderForm.find('#EditClothSelect');
        this.EditWeightFabric = this.EditOrderForm.find('#EditWeightFabric');
        this.EditCotton = this.EditOrderForm.find('#EditCotton');
        this.EditPolister = this.EditOrderForm.find('#EditPolister');
        this.EditBtn = this.EditOrderForm.find('.editBtn');


        return this;
    }

    bindEvents() {

        this.EditBtn.click((event) => {
            const url = '/CustomerOrder/EditBigOrder'
            this.EditForm(url);
            return false;

        });


        this.AddBigOrder.click((event) => {
            const url = '/CustomerOrder/SetBigOrder'
            this.AddForm(url);
            return false;

        });

        $(document).on("click", ".delete-big-order", (event) => {
            event.preventDefault();
            const removeCard = $(event.target).closest("tr");
            this.deleteBigOrder(removeCard);
            return false;
        });

        $(document).on('click', '.edit-big-order', (e) => {
            this.selectedCard = $(e.target).closest("tr");
            const Id = this.selectedCard.data('id');
            const ClothName = this.selectedCard.data('typefabricid');
            const CustomerName = this.selectedCard.data('customerid');
            const WeightFabric = this.selectedCard.data('weight');
            const CottonPercent = this.selectedCard.data('cotton');
            const PolisterPercent = this.selectedCard.data('polister');
            this.EditClothSelect.val(ClothName);
            this.EditCustomerSelect.val(CustomerName);
            this.EditWeightFabric.val(WeightFabric);
            this.EditCotton.val(CottonPercent);
            this.EditPolister.val(PolisterPercent);
            $('#Id').val(Id);
            this.EditOrderModal.modal('show');
        })

        $(document).on('click', '.add-color', (e) => {
            this.addFormColor.get(0).reset();
            this.AddColorModal.modal('show');
        });
    }


    deleteBigOrder(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });
        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد حذف هذه الطلبية الاساسية ؟",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'نعم!',
            cancelButtonText: 'إلغاء',
            reverseButtons: false

        }).then((result) => {
            if (result.value) {
                this.submitDeleteBigOrder(removedCard);
                swalWithBootstrapButtons.fire(
                    'تم!',
                    'لقد تم حذف الطلبية الأساسية',
                    'success'
                )
            }
        });
    }



    submitDeleteBigOrder(removedCard) {
        const removedBigOrderId = removedCard.data("id");
        $.ajax({
            url: `/CustomerOrder/RemoveBigOrder/${removedBigOrderId}`,
            type: "Delete",
            dataType: "json"
        }).done((json) => {
            if (json === false)
            {
                Swal.fire({
                    icon: "warning",
                    timer: 3000,
                    title: "تنبيه",
                    text: "لا يمكنك حذف هذه الطلبية لأن هناك طلبيات فرعية متعلقة بها"
                });
                return false;
            }
            let BigOrderTable = $('#BigOrderTable').DataTable();
            BigOrderTable.row($('.id-' + removedBigOrderId)).remove().draw(false);
        }).fail((xhr, status, errorThrown) => {

        }).always((xhr, status) => {

        });
    }

    EditForm(url)
    {
        if (this.EditWeightFabric.val() === "" ||
            parseFloat(this.EditWeightFabric.val()) === 0 ||
            parseFloat(this.EditWeightFabric.val()) < 0)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة وزن القماش"
            });
            return false;
        }

        if (parseFloat(this.EditCotton.val()) === 0 ||
            parseFloat(this.EditCotton.val()) < 0 ||
            parseFloat(this.EditCotton.val()) > 100) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة نسبة القطن"
            });
            return false;
        }

        if (parseFloat(this.EditPolister.val()) === 0 ||
            parseFloat(this.EditPolister.val()) < 0 ||
            parseFloat(this.EditPolister.val()) > 100) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة نسبة البوليستر"
            });
            return false;
        }
        var formBigOrder = new FormData(this.EditOrderForm[0]);
        $.ajax({
            url: url,
            data: formBigOrder,
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
            this.selectedCard.data('typefabricid', json.clothId);
            this.selectedCard.data('customerid', json.customerId);
            this.selectedCard.data('weight', json.weight);
            this.selectedCard.data('cotton', json.percentCotton)
            this.selectedCard.data('polister', json.percentPolister)

            this.selectedCard.find('.ColthName').html(json.clothName);
            this.selectedCard.find('.CustomerName').html(json.customerName);
            this.selectedCard.find('.Weight').html(json.weight);
            this.selectedCard.find('.PercentCotton').html(json.percentCotton);
            this.selectedCard.find('.PercentPolister').html(json.percentPolister);
        })
    }


    AddForm(url) {
        if (this.FabricWeight.val() === "" ||
            parseFloat(this.FabricWeight.val()) === 0 ||
            parseFloat(this.FabricWeight.val()) < 0)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة وزن القماش"
            });
            return false;
        }

        if (parseFloat(this.cotton.val()) === 0 ||
            parseFloat(this.cotton.val()) < 0 ||
            parseFloat(this.cotton.val()) > 100)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة نسبة القطن"
            });
            return false;
        }

        if (parseFloat(this.polister.val()) === 0 ||
            parseFloat(this.polister.val()) < 0 ||
            parseFloat(this.polister.val()) > 100)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة نسبة البوليستر"
            });
            return false;
        }

        var formBigOrder = new FormData(this.AddOrderForm[0]);
        $.ajax({
            url: url,
            data: formBigOrder,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) =>
        {
            $('.close').click();
            $('body').css({
                padding: 0
            })

                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'تم إضافة طلبية جديدة بنجاح ',
                    showConfirmButton: false,
                    timer: 3000,
                });
            this.AddRow(json);
            this.AddOrderForm.get(0).reset();
           

        })
    }

    AddRow(json) {
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var TodayDate =
            (day < 10 ? '0' : '') + day + '-' +
            (month < 10 ? '0' : '') + month + '-' +
            d.getFullYear();
        var ColorTable = $('#BigOrderTable').DataTable();
        ColorTable.row.add($(` <tr data-id="${json.id}" data-typefabricid="${json.clothId}"
                                                    data-customerId="${json.customerId}" data-weight="${json.weight}"
                                                    data-cotton="${json.percentCotton}" data-polister="${json.percentPolister}"
                                                    role="row" class="odd id-${json.id}">
                                                    <td>${json.id}</td>
                                                    <td class="ColthName">${json.clothName}</td>
                                                    <td class="CustomerName">${json.customerName}</td>
                                                    <td class="Weight">${json.weight}</td>
                                                    <td>${TodayDate}</td>
                                                    <td class="PercentCotton">${json.percentCotton}</td>
                                                    <td class="PercentPolister">${json.percentPolister}</td>
                                                    <td>
                                                        <a class="feather icon-edit-1 edit-big-order">
                                                        </a>
                                                    </td>
                                                    <td><a class="btn trash delete-big-order" style="color:red"><i class="fa fa-remove"></i></a></td>
                                                </tr> `)).draw(false);
    }
}

$(document).ready(() => {
    new bigOrder()
        .initializeView()
        .bindEvents();
})