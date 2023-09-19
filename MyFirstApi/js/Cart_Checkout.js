document.addEventListener("DOMContentLoaded",function(){
    //console.log("domcument")

    //初始化城市鄉鎮連動式下拉清單
    initCitySelector();
});

let cityArr =null; // 擷取到的城市鄉鎮資料

function initDistrictSelect(){
    let districtSelect = document.getElementById("districtId");
    districtSelect.innerHTML="";

    // 取得目前選擇的城市id
    let cityId = document.getElementById("cityId").value;
    if(cityId <= 0){
        return;
    }

    // 取得此城市的鄉鎮資料
    let city =  cityArr.find(function(city){
        return city.id == cityId;
    });
    let districtArr = city.districts; // 此城市的鄉鎮資料

    let dataSource = [{id : 0, name : "請選擇鄉鎮"},...districtArr];
    
    dataSource.map(function(district){
        let option = document.createElement("option");
        option.value = district.id;
        option.textContent = district.name;
        return option;
    }).forEach(function(option){
        districtSelect.appendChild(option);
    });
}

function initCitySelector(){
    let citySelect = document.getElementById("cityId");
    citySelect.addEventListener("change", initDistrictSelect)
    let districtSelect = document.getElementById("districtId");

    // 清空二者
    citySelect.innerHTML="";
    districtSelect.innerHTML="";

    //取得城市鄉鎮資料
    let url = '/api/CitiesApi';
    fetch(url,{
        method: 'GET',
        headers:{
            'Content-Type':'application/json'
        }
    })
    .then(function(response){
        return response.json();
    })
    .then(function(citiesObj){
        cityArr = citiesObj;
        //console.log("citiesObj=", citiesObj);

        // 在citiesObj前面加入一個空白選項
        let cities = [{id:0, name:"請選擇城市"},...citiesObj];
        cities.map(function(city){
            let option = document.createElement("option");
            option.value = city.id;
            option.textContent = city.name;
            return option;
        }).forEach(function(option){
            citySelect.appendChild(option);
        });
    });
}