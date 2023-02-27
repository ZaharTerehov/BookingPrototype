function onInputFileChange() {
    document.querySelector('.file-error-message').innerHTML = '';
}

function onCreateApartmentSubmit() {   
    const input = document.getElementById("input-picture");
    const files = document.getElementById("input-picture").files;
    if (files.length == 0) {
        document.querySelector('.file-error-message').innerHTML = 'No files selected';
    }
    else {
        const maximumSize = 2 * 1024 * 1024 // In MegaBytes
        for (const file of files) {
            if (file.size > maximumSize) {
                input.value = '';
                document.querySelector('.file-error-message').innerHTML = 'Files size must be less then 2 MB. No files selected';
                break;
            }                
        }
    }
}