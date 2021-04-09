const url = "https://localhost:5001/api/beanvariety/";

const target = document.querySelector("#display-beans")

const beanHTML = (variety) => {
    return `<section class="variety_container">
    <div class="variety_name"><h2>${variety.name}</h2></div>
        <div class="variety_region">Region: ${variety.region}</div>
        <div class="variety_crime">Notes: ${variety.notes}</div>
    </section>
    `
}

const renderBean = (beanVarieties) => {
    let beanCards = [];
    for (const bean of beanVarieties) {
        beanCards.push(beanHTML(bean))
    }
    target.innerHTML = beanCards.join("")
}

const button = document.querySelector("#run-button");
button.addEventListener("click", () => {
    getAllBeanVarieties()
        .then(beanVarieties => {
            console.log(beanVarieties);
            renderBean(beanVarieties)
        })
});

function getAllBeanVarieties() {
    return fetch(url).then(resp => resp.json());
}


const addBeanButton = document.querySelector("#saveNewBeanVariety");

addBeanButton.addEventListener("click", () => {
    const name = document.querySelector("#name").value
    const region = document.querySelector("#region").value
    const notes = document.querySelector("#notes").value
    const newBean = {
        name,
        region,
        notes,
    }
    debugger
    addNewBean(newBean)
        .then(beanVarieties => {
            console.log(beanVarieties)
        })
});

function addNewBean(newBean) {
    return fetch("https://localhost:5001/api/BeanVariety/", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newBean)
    }).then(getAllBeanVarieties)
}

/*
const eventHub = document.querySelector(".newBeanVarietyForm")

eventHub.addEventListener("click", clickEvent => {
    if (clickEvent.target.id === "saveNewBeanVariety") {
        const
    }
})
*/






/*
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
*/