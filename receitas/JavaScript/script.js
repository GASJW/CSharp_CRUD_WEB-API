function validar(){

    var input1 = document.getElementById("txtNome");
    var input2 = document.getElementById("txtIngredientes");
    var input3 = document.getElementById("txtMododepreparo");

    if(input1.value==""&&input2.value==""&&input3.value==""){
        alert("Todos os campos precisam ser preenchidos.")
    }
    else if(input1.value==""&&input2.value==""){
        alert("Os campos 'Nome' e 'Ingredientes' precisam ser preenchidos.")
    }
    else if(input1.value==""&&input3.value==""){
        alert("Os campos 'Nome' e 'Modo de Preparo' precisam ser preenchidos.")
    }
    else if(input2.value==""&&input3.value==""){
        alert("Os campos 'Ingredientes' e 'Modo de Preparo' precisas ser preenchidos.")
    }
    else if(input1.value==""){
        alert("O campo 'Nome' precisa ser preenchido.")
    }
    else if(input2.value==""){
        alert("O campo 'Ingredientes' precisa ser preenchido.")
    }
    else if(input3.value==""){
        alert("O campo 'Modo de Preparo' precisa ser preenchido.")
    }

    /*let input = document.getElementById("txtnome");
    let input1 = document.getElementById("txtIgredientes");
    let input2 = document.getElementById("txtMododepreparo");
    let msg = ""
    if(input.value==""){
    //alert("O campo Nome não pode ser em branco.")
        msg += "Nome embranco. \n"
   }
   if(input1.value ==""){
       //alert("O campo Ingredientes não pode ser em branco.")
       msg += "Ingrediente embranco. \n"
   }
   if(input2.value ==""){
    //alert("O campo Modo de Preparo não pode ser em branco.")
     msg += "Modo de preparo embranco."
    }

    alert(msg)*/
}