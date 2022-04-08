if (document.querySelector(".operation-message") != null) {
    const operationMessage = document.querySelector(".operation-message")
    let timeout = setTimeout(() => {
        operationMessage.remove();
    }, 1800)
}
