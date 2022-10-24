
using ApiDirectoyABM;
using ApiDirectoyABM.Modelos;
using ApiDirectoyABM.Servicios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Online...");
app.MapGet("/help", () => JsonConvert.SerializeObject(new {saludo = "Estoy vivo!!!"}));

app.MapPost("/CreateFolder/{nameFolder}",(string nameFolder) =>
{
    string response = "";
    try{
        Directory.CreateDirectory(Constans.PATH_FOLDER_DIRECTORY + "/" + nameFolder);
        response = "Carpeta Creada";
    }
    catch (Exception ex){
        response = "Error al crear la caperta:\n"+ex.Message;
    }
    return response;
});

app.MapGet("/getDirectoryes", () => {
    List<string> listDirectorys = Directory.GetDirectories(Constans.PATH_FOLDER_DIRECTORY).ToList();
    List<string> listResponse = new List<string>();
    listDirectorys.ForEach(item => listResponse.Add(item.Replace(Constans.PATH_FOLDER_DIRECTORY+"\\", "")));
    string responseJson = JsonConvert.SerializeObject(new { lista = listResponse });
    return responseJson;
});

app.MapGet("/getFiles", () =>
{
    List<string> filesResponse =  Directory.GetFiles(Constans.PATH_FOLDER_DIRECTORY).ToList();
    return JsonConvert.SerializeObject(filesResponse);
});

app.MapPost("/getAll", ([FromBody] PathPosition position) => {
    DirectoryService directoryService = new DirectoryService();
    PageDirectory pageDirectory = directoryService.getAllDirectorys(position.currentPath);
    string responseJson = JsonConvert.SerializeObject(pageDirectory);
    return responseJson;
});

app.MapPost("/imageSave", ([FromBody] ImageObj imageObj) => {
    ImageService imageService = new ImageService();
    imageService.SaveImage(imageObj);
    return imageObj;

});

/*Operaciones sobre archivos*/

app.MapPost("/toRenameFile", ([FromBody] FileRename fileRename) => { 
    FilesService filesService = new FilesService();
    Result result = new Result();

    result = filesService.RenameFile(fileRename);
    return result;
});

app.MapPost("/toPostFile", ([FromBody] FileObj fileObj) =>{
    FilesService filesService = new FilesService();
    Result result = new Result();

    result = filesService.ToPostFile(fileObj);
    return result;
});

app.MapDelete("/toDeleteFile",([FromBody] FileObj fileObj) => { 
    FilesService filesService= new FilesService();
    Result result = new Result();

    result = filesService.toDelete(fileObj);
    return result;
});





app.Run();
