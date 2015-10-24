# cSharp

Para cuando ya has commitado alguna vez en Visual Studio 2015 y quieres que el .gitignore afecte a todos los archivos antiguos no solo a los nuevos: **git rm --cached**. Hay que tener cuidado que te borra todos los archivos locales de ese proyecto, asique hay que hacer backup de los archivos q no hayas commitado. Despues, se realiza un **git pull** para bajarselo de nuevo.
