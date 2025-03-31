document.addEventListener('DOMContentLoaded', function () {
    fetchProducts();
    document.getElementById('btnAdd').addEventListener('click', addProduct);
    document.getElementById('btnUpdate').addEventListener('click', updateProduct);  // Add event listener for update button
    document.getElementById('btnClear').addEventListener('click', deleteProduct); // Add event listener for delete button

    // Event delegation for dynamic buttons
    document.getElementById('productList').addEventListener('click', function (event) {
        if (event.target.classList.contains('delete-btn')) {
            deleteProduct(event.target.dataset.id);
        } else if (event.target.classList.contains('edit-btn')) {
            getProductById(event.target.dataset.id);  // Populate form for editing
        } else if (event.target.classList.contains('view-btn')) {
            viewProductDetails(event.target.dataset.id);
        }
    });

    // Initially hide update and delete buttons
    document.getElementById('btnUpdate').style.display = 'none';
    document.getElementById('btnClear').style.display = 'none';

});

let selectedProductId = null;  // Store the ID of the product being edited


function fetchProducts() {
    const apiUrl = 'http://localhost:5136/api/products';
    fetch(apiUrl)
        .then(handleResponse)
        .then(data => displayProducts(data))
        .catch(error => console.error('Fetch error:', error.message));
}

// Handle fetch response, check for error, and parse JSON
function handleResponse(response) {
    if (!response.ok) throw new Error('Network response was not ok');
    return response.json();
}

// Display products in the HTML table
function displayProducts(products) {
    const productList = document.getElementById('productList');
    productList.innerHTML = ''; // Clear existing products

    if (products.length === 0) {
        productList.innerHTML = '<tr><td colspan="5">No products available</td></tr>';
        return;
    }

    products.forEach(product => {
        productList.innerHTML += createProductRow(product);
    });
}

// Create HTML table row for a product
function createProductRow(product) {
    return `
<tr>
<td>${product.id}</td>
<td>${product.name}</td>
<td>${product.price}</td>
<td>${product.description}</td>
<td>

<button class="btn btn-danger delete-btn" data-id="${product.id}">Delete</button>

<button class="btn btn-warning edit-btn" data-id="${product.id}">Edit</button>

<button class="btn btn-primary view-btn" data-id="${product.id}">View</button>

</td>
</tr>
`;
}

// Add a new product
function addProduct() {
    const productData = {
        name: document.getElementById('bookName').value,
        price: document.getElementById('price').value,
        description: document.getElementById('description').value,
    };
    fetch('http://localhost:5136/api/products', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(productData),
    })
        .then(handleResponse)
        .then(data => {
            console.log('Product added:', data);
            fetchProducts(); // Refresh the product list
            clearForm();
            Swal.fire('Success', 'Product added successfully!', 'success');

        })
        .catch(error => {
            console.error('Error:', error);
            Swal.fire('Error', 'Failed to add product!', 'error');
        });
}

// Get product by ID (for editing)
function getProductById(id) {
    fetch(`http://localhost:5136/api/products/${id}`)
        .then(handleResponse)
        .then(product => {
            document.getElementById('bookName').value = product.name;
            document.getElementById('price').value = product.price;
            document.getElementById('description').value = product.description;

            // Store the product ID for updating
            selectedProductId = product.id;

            // Show the update and delete buttons, hide the add button
            document.getElementById('btnUpdate').style.display = 'inline-block';
            document.getElementById('btnClear').style.display = 'inline-block';
            document.getElementById('btnAdd').style.display = 'none';

        })
        .catch(error => {
            console.error('Error:', error);
            Swal.fire('Error', 'Failed to fetch product for editing!', 'error');
        });
}

// Update product
function updateProduct() {
    if (!selectedProductId) {
        Swal.fire('Error', 'No product selected for updating!', 'error');
        return;
    }

    const productData = {
        id: selectedProductId,
        name: document.getElementById('bookName').value,
        price: document.getElementById('price').value,
        description: document.getElementById('description').value,
    };

    fetch(`http://localhost:5136/api/products/${selectedProductId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(productData),
    })
        .then(handleResponse)
        .then(data => {
            console.log('Product updated:', data);
            fetchProducts(); // Refresh the product list
            clearForm();
            Swal.fire('Success', 'Product updated successfully!', 'success');

            // Reset the buttons to original state
            document.getElementById('btnUpdate').style.display = 'none';
            document.getElementById('btnClear').style.display = 'none';
            document.getElementById('btnAdd').style.display = 'inline-block';
            selectedProductId = null; // Clear selected product ID
        })
        .catch(error => {
            console.error('Error:', error);
            Swal.fire('Error', 'Failed to update product!', 'error');
        });
}


// Delete product
function deleteProduct(id) {
    Swal.fire({
        title: 'Bạn có chắc chắn muốn xóa?',
        text: "Hành động này không thể hoàn tác!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Có, xóa ngay!',
        cancelButtonText: 'Hủy'
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(`http://localhost:5136/api/products/${id}`, {
                method: 'DELETE',
            })
            .then(response => {
                if (!response.ok) throw new Error('Xóa sản phẩm thất bại.');
                
                return response.text(); // Đảm bảo không gặp lỗi khi parse JSON
            })
            .then(() => {
                Swal.fire(
                    'Đã xóa!',
                    'Sản phẩm của bạn đã bị xóa.',
                    'success'
                );
                fetchProducts(); // Cập nhật danh sách sau khi xóa
                clearForm(); // Xóa dữ liệu trong form
                document.getElementById('btnUpdate').style.display = 'none';
                document.getElementById('btnClear').style.display = 'none';
                document.getElementById('btnAdd').style.display = 'inline-block';
            })
            .catch(error => {
                console.error('Lỗi khi xóa:', error);
                Swal.fire('Lỗi', 'Không thể xóa sản phẩm!', 'error');
            });
        }
    });
}



// View product details in modal
function viewProductDetails(id) {
    fetch(`http://localhost:5136/api/products/${id}`)
        .then(handleResponse)
        .then(product => {
            // Populate modal with product details
            document.querySelector('#modalViewDetailInfo .code[data-atr="id"]').textContent = product.id;
            document.querySelector('#modalViewDetailInfo .dateOfBirth[data-atr="bookname"]').textContent = product.name;
            document.querySelector('#modalViewDetailInfo .gender[data-atr="description"]').textContent = product.description;
            document.querySelector('#modalViewDetailInfo .fullName[data-atr="bookname"]').textContent = product.name;

            // Show the modal
            const modal = new bootstrap.Modal(document.getElementById('modalViewDetailInfo'));
            modal.show();

        })
        .catch(error => {
            console.error('Error:', error);
            Swal.fire('Error', 'Failed to fetch product details!', 'error');
        });
}


// Clear the form
function clearForm() {
    document.getElementById('bookName').value = '';
    document.getElementById('price').value = '';
    document.getElementById('description').value = '';
}