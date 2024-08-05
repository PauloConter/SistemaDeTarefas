$(document).ready(function () {
    
    function carregarTarefas() {
        $.ajax({
            url: 'https://localhost:44388/api/Tarefas',
            method: 'GET',
            success: function (tarefas) {
                $('#tarefas-lista').empty();
                tarefas.forEach(function (tarefa) {
                    $('#tarefas-lista').append(
                        `<li class="list-group-item" data-id="${tarefa.id}">${tarefa.titulo}</li>`
                    );
                });
            },
            error: function (error) {
                console.error('Erro ao carregar tarefas:', error);
            }
        });
    }
    
    function mostrarDetalhes(id) {
        $.ajax({
            url: `https://localhost:44388/api/Tarefas/${id}`,
            method: 'GET',
            success: function (tarefa) {
                $('#tarefa-detalhes').html(
                    `<h5>${tarefa.titulo}</h5>
                     <p>${tarefa.descricao}</p>
                     <p><strong>Data de Criação:</strong> ${tarefa.dataCriacao}</p>
                     <p><strong>Data de Conclusão:</strong> ${tarefa.dataConclusao}</p>
                     <p><strong>Usuário:</strong> ${tarefa.usuario ? tarefa.usuario.nome : 'N/A'}</p>
                     <p><strong>Categoria:</strong> ${tarefa.categoria ? tarefa.categoria.nome : 'N/A'}</p>`
                );
            },
            error: function (error) {
                console.error('Erro ao carregar detalhes da tarefa:', error);
            }
        });
    }
    
    carregarTarefas();
    
    $('#tarefas-lista').on('click', '.list-group-item', function () {
        var tarefaId = $(this).data('id');
        mostrarDetalhes(tarefaId);
    });
});
