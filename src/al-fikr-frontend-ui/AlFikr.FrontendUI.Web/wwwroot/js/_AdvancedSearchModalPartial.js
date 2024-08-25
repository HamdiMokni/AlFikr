var rowCount = 1; // Initialize row counter

function ManageRow(button) {
    if (button.className === 'fa fa-plus-circle') {
        // Add new row
        AddRow();
    } else {
        // Remove current row
        var row = button.closest('.advancedsearch');
        row.parentNode.removeChild(row);
    }
}

function AddRow() {
    // Clone the existing search row
    var newRow = document.querySelector('#advancedsearch').cloneNode(true);

    // Generate a unique ID for the new row
    var newRowId = 'advancedsearch-' + rowCount;
    newRow.setAttribute('id', newRowId);
    newRow.classList.add("advancedsearch");

    // Change the button to a removal button
    var button = newRow.querySelector('.actions');
    button.innerHTML = '<i class="fa fa-minus-circle" onclick="ManageRow(this)" style="cursor:pointer;font-size:30px;"></i>';

    // Update asp-for attributes for new row elements to use dynamic indexing
    const newInputs = newRow.querySelectorAll("input, select");
    for (const input of newInputs) {
        const name = input.name;
        const newName = name.replace(/\[(\d+)\]/, `[${rowCount}]`);
        input.setAttribute("name", newName);
    }

    // Append the new row to the advancedsearch container
    document.getElementById('advancedSearchRows').appendChild(newRow);

    // Increment row counter
    rowCount++;
}

function DeleteAllRows() {
    // Get all elements with the specified class name
    var elements = document.querySelectorAll('.advancedsearch');

    // Iterate over the NodeList and remove each element
    elements.forEach(function (element) {
        element.parentNode.removeChild(element);
    });

    rowCount = 1;
}