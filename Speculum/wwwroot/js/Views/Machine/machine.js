class Machine {
    initializeView() {
        this.MachineModal = $('#MachineModal');
        this.FormMachine = this.MachineModal.find('#FormMachine');
        this.MachineName = this.FormMachine.find('#machine-name');
        this.MachineType = this.FormMachine.find('#machine-type');
        this.SaveBtn = this.FormMachine.find('.save-btn');
        return this;
    }
    bindEvents() {
        this.SaveBtn.click((event) => {
            const url = '/Machine/SetMachine'
            console.log("Access");
            this.SaveForm(url);
            return false;

        });

        $(document).on('click', '.add-btn', (e) => {
            this.FormMachine.get(0).reset();
            this.MachineModal.modal('show');
        })

        $(document).on("click", ".delete-machine", (event) => {
            event.preventDefault();
            const removeCard = $(event.target).closest("tr");
            this.deleteMachine(removeCard);
            return false;
        });


        $(document).on('click', '.edit-btn', (e) => {
            this.selectedCard = $(e.target).closest("tr");
            const Id = this.selectedCard.data('id');
            const Name = this.selectedCard.data('name');
            const TypeMachine = this.selectedCard.data('type');
            console.log(Id);
            $('#Id').val(Id);
            this.MachineName.val(Name);
            this.MachineType.val(TypeMachine);
            this.MachineModal.modal('show');
        })
    }


    deleteMachine(removedCard) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        });
        swalWithBootstrapButtons.fire({
            title: "هل أنت متأكد؟",
            text: "هل فعلاً تريد حذف الماكينة ؟",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: 'نعم!',
            cancelButtonText: 'إلغاء',
            reverseButtons: false

        }).then((result) => {
            if (result.value) {
                this.submitDeleteMachine(removedCard);
                swalWithBootstrapButtons.fire(
                    'تم!',
                    'لقد تم حذف الماكينة',
                    'success'
                )
            }
        });
    }

    submitDeleteMachine(removedCard) {
        const removedMachineId = removedCard.data("id");
        console.log(removedMachineId);
        $.ajax({
            url: `/Machine/RemoveMachine/${removedMachineId}`,
            type: "Delete",
            dataType: "json"
        }).done((json) => {
            let MachineTable = $('#MachineTable').DataTable();
            MachineTable.row($('.id-' + removedMachineId)).remove().draw(false);
        }).fail((xhr, status, errorThrown) => {
        }).always((xhr, status) => {
        });
    }

    SaveForm(url) {

        if (this.MachineName.val().trim() === "")
        {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى كتابة اسم الماكينة"
            });
            return false;
        }

        if (this.MachineType.val().trim() === "") {
            Swal.fire({
                icon: "warning",
                timer: 3000,
                title: "تنبيه",
                text: "يرجى كتابة نوع الماكينة"
            });
            return false;
        }


        var formMachine = new FormData(this.FormMachine[0]);
        $.ajax({
            url: url,
            data: formMachine,
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
                    text: "لا يمكنك إضافة هذه الماكينة فهي موجودة مسبقا"
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
                    title: 'تم إضافة ماكينة جديدة بنجاح ',
                    showConfirmButton: false,
                    timer: 3000,
                });
                this.AddRow(json);
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
                this.selectedCard.data('type', json.machineType);

                this.selectedCard.find('.Machine-Name').html(json.name);
                this.selectedCard.find('.Machine-Type').html(json.machineType);
            }
        })

    }

    AddRow(json) {
        var MachineTable = $('#MachineTable').DataTable();
        MachineTable.row.add($(`  <tr data-id="${json.id}" data-name="${json.name}" 
                                                data-type="${json.machineType}" role="row" class="odd id-${json.id}">
                                                <td>${json.id}</td>
                                                <td class="Machine-Name">${json.name}</td>
                                                <td class="Machine-Type">${json.machineType}</td>
                                                <td><a class="feather icon-edit-1 edit-btn"></a></td>
                                                <td><a class="btn trash delete-machine" style="color:red"><i class="fa fa-remove"></i></a></td>
                                            </tr> `)).draw(false);
    }
}


$(document).ready(() => {
    new Machine()
        .initializeView()
        .bindEvents();
})


