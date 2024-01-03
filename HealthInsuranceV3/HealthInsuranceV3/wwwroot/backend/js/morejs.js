﻿function ChangeToSlug() {

    var slug;

    //Lấy text từ thẻ input title 
    slug = document.getElementById("title").value;
    slug = slug.toLowerCase();
    //Đổi ký tự có dấu thành không dấu
    slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
    slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    slug = slug.replace(/đ/gi, 'd');
    //Xóa các ký tự đặt biệt
    slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
    //Đổi khoảng trắng thành ký tự gạch ngang
    slug = slug.replace(/ /gi, "-");
    //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
    //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
    slug = slug.replace(/\-\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-/gi, '-');
    slug = slug.replace(/\-\-/gi, '-');
    //Xóa các ký tự gạch ngang ở đầu và cuối
    slug = '@' + slug + '@';
    slug = slug.replace(/\@\-|\-\@|\@/gi, '');
    //In slug ra textbox có id “slug”
    document.getElementById('convert_slug').value = slug;
}

// Lấy các phần tử cần di chuyển
var filterElement = document.getElementById("myTable_filter");
var lengthElement = document.getElementById("myTable_length");

// Kiểm tra xem cả hai phần tử có tồn tại trong DOM không
if (filterElement && lengthElement) {
    // Di chuyển phần tử filter vào bên trong phần tử length
    lengthElement.appendChild(filterElement);
}

// toast

function addCategory() {
    console.log("ddddds");
    // Lấy giá trị từ các trường input
    var name = document.getElementById('title').value;
    var slug = document.getElementById('convert_slug').value;
    var icon = document.getElementById('icon').value;

    // Kiểm tra nếu tên và slug không rỗng
    if (name.trim() !== '' && slug.trim() !== '') {
        alert("Ok");
        // Gọi hàm Swal.fire() để hiển thị thông báo thành công
        Swal.fire({
            title: 'Thêm danh mục thành công!',
            icon: 'success',
            confirmButtonText: 'OK'
        }).then((result) => {
            if (result.isConfirmed) {
                // Chuyển hướng về trang danh sách
                window.location.href = '/Category/Index';
            }
        });
    } else {
        // Hiển thị thông báo lỗi nếu tên hoặc slug rỗng
        Swal.fire({
            title: 'Lỗi',
            text: 'Vui lòng nhập tên và slug',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    }
}