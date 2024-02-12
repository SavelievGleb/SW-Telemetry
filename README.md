# Stormworks HTTP Telemetry
![dotnet](https://img.shields.io/badge/.NET-6.0-blue) ![npm](https://img.shields.io/badge/NPM-10.1.0-blue)
## Getting started
Устанавливаем [Node JS](https://nodejs.org/en) и [.NET 6.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.26-windows-x64-installer)

Микроконтроллеры находятся в папке `StormWorks microprocessors`, их необходимо скопировать по пути *C:\Users\Пользователь\AppData\Roaming\Stormworks\data\microprocessors*
### Запуск HTTP сервера
Заходим в папку `HTTP_Server` и запускаем `update.bat`. Должна создаться папка `node_modules`.<br>
Если папка не была создана, проверьте установку *Node JS* и повторите попытку.<br>
Далее запускаем `Start HTTP Server.bat`<br>
![server console](https://i.imgur.com/JfL3gmC.png)<br>
Если видим такую консоль, значит HTTP сервер запущен удачно<br>
### Программа визуализации
Заходим в папку `Reader prog` и запускаем `Reader.exe`<br>
При первом запуске программы необходимо будет указать где находится файл с данными, для этого нажимаем кнопку ***Select file*** и указываем файл *telemetry.txt* в папке `HTTP_Server`<br>
Адрес текущего файла указан внизу окна программы, и запоминается в настройках, так что указать файл достаточно один раз.<br>
Для обновления информации на графике достаточно нажать кнопку ***Load***. Не рекомендуется в процессе отправки данных. 