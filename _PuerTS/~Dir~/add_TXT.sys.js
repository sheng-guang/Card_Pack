var fs = require('fs');
var path = require('path');
var list=[];
function listFile(dir){
    var arr = fs.readdirSync(dir);
    arr.forEach(function(item){
        var fullpath = path.join(dir,item);
        var stats = fs.statSync(fullpath);
        if(stats.isDirectory()){
            listFile(fullpath);
            return;
        }
        if(fullpath.endsWith(".sys.js"))return;
        if(fullpath.endsWith(".js")==false)return;
        list.push(fullpath);
    });

    return list;

}
function add_TXT(list){
    list.forEach(x=>{
        console.log(x);
        fs.rename(x,x+".txt",(e)=>{
            if(e)console.log(e);
        });
    })
}
listFile("./")
console.log(list)
add_TXT(list)
