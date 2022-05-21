function getDescricaoErro(erro){
    let descricaoErro = "";
    switch (erro){
        case 1040:
            descricaoErro = "NÃO ENCONTRADO";
            break;
        case 1004:
            descricaoErro = "NÃO EXISTEM DADOS PARA EXIBIR";
            break;
        case 5000:
            descricaoErro = "ERRO INTERNO NO SERVIDOR";
            break;
        case 4000:
            descricaoErro = "REQUISIÇÃO DE ENTRADA INVÁLIDA";
            break;
        case -1:
            descricaoErro = "ERRO DESCONHECIDO";
            break;
        default:
            descricaoErro = `ERRO DESCONHECIDO: ${erro}`;
            break;
    }
    return descricaoErro;
}