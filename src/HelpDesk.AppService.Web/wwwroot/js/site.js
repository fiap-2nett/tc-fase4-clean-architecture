// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
    
    let actionButtons = document.querySelectorAll('.hd-action-button');
    actionButtons.forEach(item => {        
        item.addEventListener('click', event => {
            event.preventDefault();
            const { ticketId, actionType } = event.target.dataset;

            $.get(`/Ticket/GetActionModal?ticketId=${ticketId}&actionType=${actionType}`)
                .done(data =>
                {
                    $('#modalActions').html(data.partialView);
                    $('#modalActions').on('shown.bs.modal', () => {
                        bindModalObjects();
                    }).modal('show');                    
                });   
        });
    });

    //let btnSubmitAction = document.querySelector('#btnSubmitAction');
    //if (btnSubmitAction.length > 0) {
    //    //btnSubmitAction.addEventListener('click', event => {
    //    //    event.preventDefault();
    //    //})
    //}


    const bindModalObjects = function () {

        let formAction = document.getElementById('formTicketAction');
        if (formAction) {
            formAction.addEventListener('submit', event => {
                event.preventDefault();
                
                const form = event.target;
                const payload = $(form).serialize();

                $.post(form.action, payload)
                    .done(data => {
                        if (data.isSuccess) {
                            location.reload(true);
                        }
                    });

            })
        }

    }



})();
