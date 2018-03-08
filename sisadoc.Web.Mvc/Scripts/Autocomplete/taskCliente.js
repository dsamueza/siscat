function ArrayCliente(){
    var arrayC
    var dataRowss = {
        'id': "A"
    }
            $.ajax({

                type: "POST",
                url: 'GetClienteArray/Docente',
                data: dataRowss,
            success: function (estado) {

                arrayC=estado;
            },
            async: false,
                cache: false,
            error: function () { }
            });
            return arrayC;
}

            $('#autocomplete-ajax').autocomplete({
                // serviceUrl: '/autosuggest/service/url',
                lookup: ArrayCliente(),
                lookupFilter: function(suggestion, originalQuery, queryLowerCase) {
                    var re = new RegExp('\\b' + $.Autocomplete.utils.escapeRegExChars(queryLowerCase), 'gi');
                    return re.test(suggestion.value);
                },
                onSelect: function(suggestion) {
                    $('#selction-ajax').html('You selected: ' + suggestion.value + ', ' + suggestion.data);
                    $('#idCliente').val(suggestion.data.toString());
                },
                onHint: function (hint) {
          
                    $('#idCliente').val(hint);
                },
                onInvalidateSelection: function() {
                    $('#selction-ajax').html('You selected: none');
                }
            });