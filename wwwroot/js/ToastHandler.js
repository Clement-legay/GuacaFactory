function ShowToast(toastElement) {
    const toast = new bootstrap.Toast(toastElement)
    toast.show()
}

function DisposeToast(toastElement) {
    const toast = bootstrap.Toast.getInstance(toastElement)
    toast.dispose()
}

function ShowModal(modalElement) {
    const modal = new bootstrap.Modal(modalElement)
    modal.show()
}

function HideModal(modalElement, backdropElement) {
    const modal = bootstrap.Modal.getInstance(modalElement)
    modal.hide()
}