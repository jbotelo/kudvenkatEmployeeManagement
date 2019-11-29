function confirmDelete(uniqueId, isDeleteClicked) {
    var delelteSpan = "deleteSpan_" + uniqueId
    var confirmDeleteSpan = "confirmDeleteSpan_" + uniqueId

    if (isDeleteClicked) {
        $('#' + delelteSpan).hide()
        $('#' + confirmDeleteSpan).show()
    } else {
        $('#' + delelteSpan).show()
        $('#' + confirmDeleteSpan).hide()
    }
}