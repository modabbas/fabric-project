class subOrder {

    initializeView()
    {
        this.AddOrderModal = $('#AddOrderModal');
        this.AddOrderForm = this.AddOrderModal.find('#AddOrderForm');
        this.OrderSelect = this.AddOrderForm.find('#OrderSelect');
        this.ColorSelect = this.AddOrderForm.find('#ColorSelect');
        this.FabricWeight = this.AddOrderForm.find('#FabricWeight');
        this.OldTall = this.AddOrderForm.find('#old-tall');
        this.ColorAmount = this.AddOrderForm.find('#color-amount');
        this.AddSmallOrder = this.AddOrderForm.find('.add-small-order');
        this.EditOrderModal = $('#EditOrderModal');
        this.EditOrderForm = this.EditOrderModal.find('#EditOrderForm');
        this.EditColorSelect = this.EditOrderForm.find('#EditColorSelect');
        this.EditOrderSelect = this.EditOrderForm.find('#EditOrderSelect');
        this.EditFabricWeight = this.EditOrderForm.find('#EditFabricWeight');
        this.EditOldTall = this.EditOrderForm.find('#edit-old-tall');
        this.EditColorAmount = this.EditOrderForm.find('#edit-color-amount');
        this.EditBtn = this.EditOrderForm.find('.editBtn');
        return this;
    }

    bindEvents() {

        this.EditBtn.click((event) => {
            const url = '/CustomerOrderDetail/EditSubOrder'
            this.EditForm(url);
            return false;
        });


        this.AddSmallOrder.click((event) => {
            const url = '/CustomerOrderDetail/SetSmallOrder'
            this.AddForm(url);
            return false;
        });

        $(document).on("click", ".delete-small-order", (event) => {
            event.preventDefault();
            const removeCard = $(event.target).closest("tr");
            this.deleteSmallOrder(removeCard);
            return false;
        });



        $(document).on("click", ".OnWork", (event) => {
            event.preventDefault();
            const removeCard = $(event.target).closest("tr");
            this.OnWorking(removeCard);
            return false;
        });





        $(document).on('click', '.edit-small-order', (e) => {
            this.selectedCard = $(e.target).closest("tr");
            const Id = this.selectedCard.data('id');
            const ColorId = this.selectedCard.data('colorid');
            const CustomerOrderId = this.selectedCard.data('customerorderid');
            const PartialFabricWeight = this.selectedCard.data('partialweight');
            const OldTall = this.selectedCard.data('oldtall');
            const ColorAmount = this.selectedCard.data('coloramount');
            this.EditColorSelect.val(ColorId);
            this.EditOrderSelect.val(CustomerOrderId);
            this.EditFabricWeight.val(PartialFabricWeight);
            this.EditOldTall.val(OldTall);
            this.EditColorAmount.val(ColorAmount);
            $('#Id').val(Id);
            this.EditOrderModal.modal('show');
        })

        $(document).on('click', '.add-color', (e) => {
            this.addFormColor.get(0).reset();
            this.AddColorModal.modal('show');
        });
    }


    OnWorking(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });
        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد نقل هذه الطلبية الفرعية ؟",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'نعم!',
            cancelButtonText: 'إلغاء',
            reverseButtons: false

        }).then((result) => {
            if (result.value) {
                this.submitWorkOrder(removedCard);
                swalWithBootstrapButtons.fire(
                    'تم!',
                    'لقد تم نقل هذه الطلبية ينجاح',
                    'success'
                )
            }
        });
    }


    deleteSmallOrder(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });
        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد حذف هذه الطلبية الفرعية ؟",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'نعم!',
            cancelButtonText: 'إلغاء',
            reverseButtons: false

        }).then((result) => {
            if (result.value) {
                this.submitDeleteSmallOrder(removedCard);
                swalWithBootstrapButtons.fire(
                    'تم!',
                    'لقد تم حذف الطلبية الفرعية',
                    'success'
                )
            }
        });
    }


    submitWorkOrder(removedCard) {
        const SmallOrderIdOnWork = removedCard.data("id");
        $.ajax({
            url: `/CustomerOrderDetail/RemoveOrderFromDeliver/${SmallOrderIdOnWork}`,
            type: "PUT",
            dataType: "json"
        }).done((json) => {
            let SmallOrderTable = $('#SmallOrderTable').DataTable();
            SmallOrderTable .row($('.id-' + SmallOrderIdOnWork)).remove().draw(false);
        }).fail((xhr, status, errorThrown) => {

        }).always((xhr, status) => {

        });
    }


    submitDeleteSmallOrder(removedCard) {
        const removedSmallOrderId = removedCard.data("id");
        $.ajax({
            url: `/CustomerOrderDetail/RemoveSubOrder/${removedSmallOrderId}`,
            type: "Delete",
            dataType: "json"
        }).done((json) => {
            console.log(json);
            $('#order-' + json.orderId).val(json.weightReturn);
            $('#color-' + json.colorId).val(json.amountReturn);
            let SmallOrderTable = $('#SmallOrderTable').DataTable();
            SmallOrderTable.row($('.id-' + removedSmallOrderId)).remove().draw(false);
        }).fail((xhr, status, errorThrown) => {

        }).always((xhr, status) => {

        });
    }

    EditForm(url) {
        if (this.EditFabricWeight.val() === "" ||
            parseInt(this.EditFabricWeight.val()) === 0 ||
            parseInt(this.EditFabricWeight.val()) < 0)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة وزن القماش"
            });
            return false;
        }
        var order = this.EditOrderSelect.val();
        var SmallOrderWeight = parseInt($('#eorder-' + order).val());
        if (parseInt(this.EditFabricWeight.val()) > SmallOrderWeight) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "وزن الطلبية الفرعية تجاوز وزن الطلبية الأساسية"
            });
            return false;
        }

        if (this.EditOldTall.val() === "" ||
            parseInt(this.EditOldTall.val()) === 0 ||
            parseInt(this.EditOldTall.val()) < 0) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة الطول القديم"
            });
            return false;
        }

        if (this.EditColorAmount.val() === "" ||
            parseInt(this.EditColorAmount.val()) === 0 ||
            parseInt(this.EditColorAmount.val()) < 0) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة كمية اللون"
            });
            return false;
        }

        var color = this.EditColorSelect.val();
        var ColorAmmount = parseInt($('#ecolor-' + color).val());
        var x = parseInt(this.EditColorAmount.val());
        if (parseInt(this.EditColorAmount.val()) > ColorAmmount) {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يجب التقليل من كمية اللون لأنك تجاوزت الحد المطلوب"
            });
            return false;
        }

        var formSubOrder = new FormData(this.EditOrderForm[0]);
        $.ajax({
            url: url,
            data: formSubOrder,
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
            location.href = `/CustomerOrderDetail/Index`;
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
        var order = this.OrderSelect.val();
        var BigOrderWeight = parseFloat($('#order-' + order).val());
        if (parseFloat(this.FabricWeight.val()) > BigOrderWeight)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "وزن الطلبية الفرعية تجاوز وزن الطلبية الأساسية"
            });
            return false;
        }

        if (this.OldTall.val() === "" ||
            parseFloat(this.OldTall.val()) === 0 ||
            parseFloat(this.OldTall.val()) < 0)
       {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة الطول القديم"
            });
            return false;
        }

        if (this.ColorAmount.val() === "" ||
            parseFloat(this.ColorAmount.val()) === 0 ||
            parseFloat(this.ColorAmount.val()) < 0)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى التأكد من صحة كمية اللون"
            });
            return false;
        }

        var color = this.ColorSelect.val();
        var ColorAmmount = parseFloat($('#color-' + color).val());
        if (parseFloat(this.ColorAmount.val()) > ColorAmmount)
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يجب التقليل من كمية اللون لأنك تجاوزت الحد المطلوب"
            });
            return false;
        }
        var formSmallOrder = new FormData(this.AddOrderForm[0]);
        $.ajax({
            url: url,
            data: formSmallOrder,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false
        }).done((json) => {
            var finalValueForAmount = ColorAmmount - json.colorAmount;
            $('#color-' + color).val(finalValueForAmount);
            var finalValueForWeight = BigOrderWeight - json.partialWeghit;
            $('#order-' + order).val(finalValueForWeight);
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
        var ColorTable = $('#SmallOrderTable').DataTable();
        ColorTable.row.add($(` <tr data-id="${json.id}" data-partialweight="${json.partialWeghit}" 
                                                data-colorid="${json.colorId}" data-customerorderid="${json.customerOrderId}"
                                                data-coloramount="${json.colorAmount}"
                                                data-oldtall="${json.oldLenght}" role="row" class="odd id-${json.id}">
                                                <td>${json.customerOrderId}</td>
                                                <td class="ClothName">${json.clothName}</td>
                                                <td class="CustomerName">${json.customerName}</td>
                                                <td class="PartialWeghit">${json.partialWeghit}</td>
                                                <td>${TodayDate}</td>
                                                <td class="ColorName">${json.colorName}</td>
                                                <td class="ColorAmount">${json.colorAmount}</td>
                                                <td class="OldLenght">${json.oldLenght}</td>
                                                <td><a class="btn trash OnWork" style="color:#28c76f"><i class="fa fa-remove"></i></a></td>
                                                <td><a class="feather icon-edit-1 edit-small-order"></a></td>
                                                <td><a class="btn trash delete-small-order" style="color:red" ><i class="fa fa-remove"></i></a></td>
                                            </tr> `)).draw(false);
    }
}

$(document).ready(() => {
    new subOrder()
        .initializeView()
        .bindEvents();
})