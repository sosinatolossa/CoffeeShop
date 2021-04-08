const url = "https://localhost:5001/api/beanvariety/";

const button = document.querySelector("#run-button");
button.addEventListener("click", () => {
    getAllBeanVarieties()
        .then(beanVarieties => {
            console.log(beanVarieties);
        })
});

function getAllBeanVarieties() {
    return fetch(url).then(resp => resp.json());
}

const url2 = "https://localhost:5001/api/coffee/";

const button2 = document.querySelector("#run-button");
button2.addEventListener("click", () => {
    getAllCoffee()
        .then(theCoffee => {
            console.log(theCoffee);
        })
});

function getAllCoffee() {
    return fetch(url2).then(resp => resp.json());
}