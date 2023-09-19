document.addEventListener("DOMContentLoaded", function(event){
    //console.log("dom loaded");
    initForm();
});

function initForm(){
    // 取得商品分類清單
    var url = "/api/CategoriesApi/";

    fetch(url, {
        method:"GET",
        headers: {
            "Content-Type":"application/json"
        }
    })
    .then(function(response){
        //console.log("response = ", response)

        return response.json();
    })
    .then(function(jsonData){
        //console.log("jsonData = ", jsonData)
        readerCategorySelect(jsonData);
    });
}

function readerCategorySelect(jsonData){
    let categorySelect = document.getElementById("categoryId");

    // 寫法1
    // let dataSource = jsonData.unshift({id:0, name:"請選擇商品分類"});

    // let options = jsonData.map(function(item){
    //     return`<option value="${item.id}">${item.name}</option>`;
    // });

    // categorySelect.innerHTML = options.join("");

    // 寫法2
    let dataSource = [{id:0, name:"請選擇商品分類"},...jsonData];
    dataSource.map(function(item){
        let option = document.createElement("option");
        option.value = item.id;
        option.textContent = item.name;
        return option;
    }).forEach(function(option){
        categorySelect.appendChild(option);
    });
}