
using ApiDirectoyABM;
using ApiDirectoyABM.Modelos;
using ApiDirectoyABM.Servicios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new() { Title = "API Control folder", Version = "v1" });
});

var app = builder.Build();

app.MapGet("/", () => {

    Debug.WriteLine("Online check");
    return "Online...";
});
app.MapGet("/help", () => JsonConvert.SerializeObject(new {saludo = "Estoy vivo!!!"}));

app.MapPost("api/create-folder/{name}",(string name) =>
{
    string response = "";
    try{
        Directory.CreateDirectory(Constans.PATH_FOLDER_DIRECTORY + "/" + name);
        response = "Carpeta Creada";
    }
    catch (Exception ex){
        response = "Error al crear la caperta:\n"+ex.Message;
    }
    return response;
});

app.MapGet("api/directories", () => {
    List<string> listDirectorys = Directory.GetDirectories(Constans.PATH_FOLDER_DIRECTORY).ToList();
    List<string> listResponse = new List<string>();
    listDirectorys.ForEach(item => listResponse.Add(item.Replace(Constans.PATH_FOLDER_DIRECTORY+"\\", "")));
    string responseJson = JsonConvert.SerializeObject(new { Directories = listResponse });
    return responseJson;
});

app.MapGet("api/files", () =>
{
    List<string> filesResponse =  Directory.GetFiles(Constans.PATH_FOLDER_DIRECTORY).ToList();
    Console.WriteLine(filesResponse);
    return JsonConvert.SerializeObject(new { Files = filesResponse });
});

app.MapPost("api/all", ([FromBody] PathPosition position) => {
    DirectoryService directoryService = new DirectoryService();
    PageDirectory pageDirectory = directoryService.getAllDirectorys(position.currentPath);
    string responseJson = JsonConvert.SerializeObject(pageDirectory);
    return responseJson;
});

app.MapPost("api/send-image", ([FromBody] ImageObj imageObj) => {
    ImageService imageService = new ImageService();
    return imageService.SaveImage(imageObj);

});

/*Operaciones sobre archivos*/

app.MapPost("api/rename-file", ([FromBody] FileRename fileRename) => { 
    FilesService filesService = new FilesService();
    Result result = new Result();

    result = filesService.RenameFile(fileRename);
    return result;
});

app.MapPost("api/post-file", ([FromBody] FileObj fileObj) =>{
    FilesService filesService = new FilesService();
    Result result = new Result();

    result = filesService.ToPostFile(fileObj);
    return result;
});

app.MapDelete("/delete-file",([FromBody] FileObj fileObj) => { 
    FilesService filesService= new FilesService();
    Result result = new Result();

    result = filesService.ToDelete(fileObj);
    return result;
});

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

app.Run();
