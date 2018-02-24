tinymce.init({
    selector: ".html",
    plugins: [
        "advlist autolink lists link image charmap print preview hr anchor pagebreak",
        "searchreplace wordcount visualblocks visualchars code fullscreen",
        "insertdatetime media nonbreaking save table contextmenu directionality",
        "emoticons template paste textcolor autoresize moxiemanager"
    ],
    relative_urls: false,
    remove_script_host: false,
    convert_urls: true,
    toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
    toolbar2: "print preview media | forecolor backcolor emoticons",
    templates: [
        {
            title: 'Template 1',
            content: '<div style="padding-left: 30px; float: left;"><img style="float: left; margin-right: 20px;" src="/Content/images/servic.PNG" alt="" width="191" height="193" />&nbsp;<strong><strong>Lorem Ipsum</strong> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry&#39;s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled iremaining essentially unchanged. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</strong></div>'
        },
        {
            title: 'Template 2',
            content: '<p style="padding-left: 30px; float: left;"><img style="float: left; margin-right: 20px;" src="/Content/images/eng1.PNG" alt="" width="191" height="193" /><img style="float: left; margin-right: 20px;" src="/Content/images/eng2.PNG" alt="" width="193" height="194" />&nbsp;<strong><strong>Lorem Ipsum</strong> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry&#39;s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled iremaining essentially unchanged. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; passages</strong></p>'
        },
        {
            title: 'Template 3',
            content: '<p style="padding-left: 30px; float: left;"><strong><strong>Lorem Ipsum</strong> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry&#39;s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled iremaining essentially unchanged. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsu&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; passages</strong></p>'
        }

    ]
});