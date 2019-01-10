const http = axios.create({
    baseURL: 'http://localhost:14491/api',
    headers: { 'Content-Type': 'application/json' },
});


http.interceptors.response.use(function (response) {
    const { data, status } = response;
    return { data, status };
}, function (error) {
    return Promise.reject(error);
});



class App {

    constructor() {
        $('.selectpicker').selectpicker();                
        this.carregarRestaurantes();
        this.exibirMensagemBoasVindas();        
    }


    exibirMensagemBoasVindas() {
        let mensagemBoasVindas = `<h6>Seja bem vindo!</h6> 
                            <p>-Insira seu nome.</p> 
                            <p>- Escolha o restaurante.</p>
                            <p> - Clique no botão votar.</p>
                            <p> - Clique no botão Resultado para ver o restaurante escolhido.</p>`;

        swal({ title: '', html: mensagemBoasVindas, type: 'info' });
    }


    async carregarRestaurantes() {
        let response = await http.get('/restaurante');
        _.forEach(response.data, (restaurante) => {
            $("#restaurante").append(`<option value="${restaurante.id}">${restaurante.nome}<option>`);
        })

        $('.selectpicker').selectpicker('refresh');
    }

    async votar() {
        try {
            let nome = $("#nome").val();
            let profissional = JSON.parse(JSON.stringify(localStorage.getItem('profissional')));
            let id = null;

            if (profissional) {
                if (profissional.profissionalNome !== nome) {
                    id = profissional.profissionalId;
                }                
            }
              
            let voto = {
                restauranteId: $("#restaurante").val(),
                profissionalNome: nome,
                profissionalId: id
            };

            let response = await http.post('/votacao', voto );

            const { profissionalId, mensagem } = response.data;

            localStorage.setItem('profissional', JSON.stringify(voto));
            swal(':)', mensagem, 'success');



        } catch (error) {
            if (error.response) {
                swal(';(', `${error.response.data}`, 'error');
                return;
            }
            
            swal(';(', 'Ocorreu um erro na votação.', 'error');
        }
    }

    async encerrarVotacao() {
        try {
     
            let response = await http.get('/votacao');

            
            const { restaurante, quantidadeDeVotos } = response.data;

            swal(':)', `O restaurante escolhido é: ${restaurante}`, 'success');

             localStorage.clear();
        } catch (error) {
            swal(';(', 'Ocorreu um erro na votação.', 'error');
        }
    }

}

var app = new App();