using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class CustomerRepository //public class CustomerRepository: Declara una clase pública llamada CustomerRepository. Esta clase encapsula las operaciones relacionadas con los clientes en la base de datos.
    {
        
        public List<Customers> ObtenerTodos() { //Define un método público que devuelve una lista de objetos Customers. Este método se utiliza para obtener todos los registros de la tabla Customers.
            using (var conexion= DataBase.GetSqlConnection()) {//using (var conexion = DataBase.GetSqlConnection()): Abre una conexión con la base de datos utilizando un objeto SqlConnection, que se obtiene mediante un método llamado GetSqlConnection de la clase DataBase. El uso del bloque using garantiza que la conexión se cerrará automáticamente cuando se salga del bloque.
                String selectFrom = ""; // Declara una variable de tipo String llamada selectFrom y la inicializa como una cadena vacía. Esta variable se usará para construir la consulta SQL.

                selectFrom = selectFrom + "SELECT [CustomerID] " + "\n"; //Esta línea inicia la construcción de una instrucción SQL SELECT. Especifica que se quiere obtener la columna CustomerID de la tabla Customers.
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n"; //Añade la columna CompanyName a la lista de columnas que se quieren seleccionar.
                selectFrom = selectFrom + "      ,[ContactName] " + "\n"; //Añade la columna ContactName a la lista de selección.
                selectFrom = selectFrom + "      ,[ContactTitle] " + "\n"; //Añade la columna ContactTitle a la lista.
                selectFrom = selectFrom + "      ,[Address] " + "\n"; //Añade la columna Address a la lista.
                selectFrom = selectFrom + "      ,[City] " + "\n"; //Añade la columna City a la lista.
                selectFrom = selectFrom + "      ,[Region] " + "\n"; //Añade la columna Region a la lista.
                selectFrom = selectFrom + "      ,[PostalCode] " + "\n"; //Añade la columna PostalCode a la lista.
                selectFrom = selectFrom + "      ,[Country] " + "\n"; //Añade la columna Country a la lista.
                selectFrom = selectFrom + "      ,[Phone] " + "\n"; //Añade la columna Phone a la lista.
                selectFrom = selectFrom + "      ,[Fax] " + "\n"; //Añade la columna Fax a la lista.
                selectFrom = selectFrom + "  FROM [dbo].[Customers]"; //Especifica la tabla de la cual se van a seleccionar las columnas ([dbo].[Customers]). [dbo] es el esquema de la base de datos, y Customers es el nombre de la tabla.

                using (SqlCommand comando = new SqlCommand(selectFrom, conexion)) { /*Crea un objeto SqlCommand: Se está creando una nueva instancia de la clase SqlCommand llamada comando.
                    Define la consulta SQL: El constructor de SqlCommand toma dos argumentos:
                    selectFrom, que es una cadena de texto que contiene la consulta SQL que se va a ejecutar.
                    conexion, que es un objeto SqlConnection que representa la conexión a la base de datos donde se ejecutará la consulta.
                    Bloque using: El bloque using asegura que el objeto comando se libere correctamente cuando se termine de usar, liberando los recursos del sistema asociados con él.*/
                    SqlDataReader reader = comando.ExecuteReader(); /*Ejecuta la consulta SQL: Se utiliza el método ExecuteReader() del objeto SqlCommand para ejecutar la consulta SQL. Este método se usa cuando se espera que la consulta devuelva un conjunto de resultados (por ejemplo, una SELECT).
                    Devuelve un SqlDataReader: ExecuteReader() devuelve un objeto SqlDataReader llamado reader, que permite leer los resultados de la consulta fila por fila. Este objeto proporciona un mecanismo para leer datos secuencialmente de la base de datos.*/
                    List<Customers> Customers = new List<Customers>(); //Crea una lista vacía de objetos Customers: Se crea una lista vacía de tipo Customers que se llamará Customers. Esta lista está diseñada para almacenar múltiples objetos Customers que serán creados a partir de los datos recuperados de la base de datos.
                    while (reader.Read()) /*Itera sobre las filas de resultados: Este bucle while se ejecuta mientras haya filas disponibles en el SqlDataReader (reader).
                    Método Read(): El método Read() avanza el reader a la siguiente fila de datos. Si hay una fila disponible, devuelve true; de lo contrario, devuelve false y el bucle se detiene.*/
                    {
                        var customers = LeerDelDataReader(reader); /*Convierte la fila de datos en un objeto Customers: Se llama al método LeerDelDataReader(reader) y se le pasa el SqlDataReader como argumento.
                        Método LeerDelDataReader(reader): Este método toma la fila actual de datos del reader y crea un nuevo objeto Customers con esos datos. Luego, asigna este objeto a la variable customers.*/
                        Customers.Add(customers); //Añade el objeto Customers a la lista: El objeto customers, que contiene los datos de una fila de la base de datos, se añade a la lista Customers. Esta lista acumula todos los objetos Customers que representan las filas de la consulta SQL.
                    }
                    return Customers; //Devuelve la lista completa de objetos Customers: Después de leer todas las filas y convertirlas en objetos Customers, se devuelve la lista Customers desde el método. Esta lista contiene todos los registros obtenidos de la base de datos.
                }
            }
           
        }
        public Customers ObtenerPorID(string id)
        { /*Declaración del método ObtenerPorID:
          Tipo de retorno Customers: Este método devuelve un objeto de tipo Customers.
          Nombre del método ObtenerPorID: El nombre del método indica que su propósito es obtener un cliente (o registro) específico por su ID.
          Parámetro id: El método toma un parámetro de tipo string llamado id. Este parámetro representa el identificador único del cliente que se desea recuperar de la base de datos.*/

            using (var conexion = DataBase.GetSqlConnection())
            { /*Establece una conexión con la base de datos:
              DataBase.GetSqlConnection(): Se llama a un método estático GetSqlConnection() de la clase DataBase, que devuelve un objeto SqlConnection representando la conexión a la base de datos. Este método probablemente configura la conexión con la cadena de conexión adecuada para la base de datos.
              Crea una variable conexion: Se asigna la conexión a la variable conexion, que se utilizará para interactuar con la base de datos dentro de este bloque.
              Bloque using: Se utiliza un bloque using para garantizar que la conexión (conexion) se cierre y libere correctamente una vez que el código dentro del bloque haya terminado de ejecutarse, sin importar si ocurrió un error o no. Esto es importante para manejar recursos de manera eficiente y evitar conexiones abiertas innecesariamente.*/

                String selectForID = ""; //Declara una variable selectForID: Se crea una cadena vacía llamada selectForID, que se utilizará para construir la consulta SQL.
                selectForID = selectForID + "SELECT [CustomerID] " + "\n"; //ñade la parte de selección de la consulta: Se concatena la primera línea de la consulta SQL, que indica que se van a seleccionar los datos de la columna [CustomerID] de la tabla.
                selectForID = selectForID + "      ,[CompanyName] " + "\n"; //Añade la columna CompanyName: Se concatena la siguiente línea para incluir la columna [CompanyName] en la selección.
                selectForID = selectForID + "      ,[ContactName] " + "\n"; //Añade la columna ContactName: Se concatena la línea para incluir la columna [ContactName].
                selectForID = selectForID + "      ,[ContactTitle] " + "\n"; //Añade la columna ContactTitle: Se concatena la línea para incluir la columna [ContactTitle].
                selectForID = selectForID + "      ,[Address] " + "\n"; //Añade la columna Address: Se concatena la línea para incluir la columna [Address].
                selectForID = selectForID + "      ,[City] " + "\n"; //Añade la columna City: Se concatena la línea para incluir la columna [City].
                selectForID = selectForID + "      ,[Region] " + "\n"; //Añade la columna Region: Se concatena la línea para incluir la columna [Region].
                selectForID = selectForID + "      ,[PostalCode] " + "\n"; //Añade la columna PostalCode: Se concatena la línea para incluir la columna [PostalCode].
                selectForID = selectForID + "      ,[Country] " + "\n"; //Añade la columna Country: Se concatena la línea para incluir la columna [Country].
                selectForID = selectForID + "      ,[Phone] " + "\n"; //Añade la columna Phone: Se concatena la línea para incluir la columna [Phone].
                selectForID = selectForID + "      ,[Fax] " + "\n"; //Añade la columna Fax: Se concatena la línea para incluir la columna [Fax].
                selectForID = selectForID + "  FROM [dbo].[Customers] " + "\n"; //Especifica la tabla de la que se seleccionarán los datos: Se concatena la línea que indica que los datos se extraerán de la tabla [dbo].[Customers].
                selectForID = selectForID + $"  Where CustomerID = @customerId"; //Añade la cláusula WHERE para filtrar por CustomerID: Se concatena la cláusula WHERE, que limita los resultados a aquellos registros donde el CustomerID coincide con el valor de @customerId. Este es un parámetro que se pasará al comando SQL más adelante, lo que permite evitar inyecciones SQL y mejorar la seguridad.

                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                { /*Crea un objeto SqlCommand:
                  Se inicializa un nuevo objeto SqlCommand llamado comando, que se utiliza para ejecutar la consulta SQL que se ha construido en la variable selectForID.
                  Parámetros:
                  selectForID: Esta es la consulta SQL que se ejecutará. Incluye la instrucción SELECT que hemos creado anteriormente.
                  conexion: Este es el objeto de conexión a la base de datos que se abrió previamente. Permite al SqlCommand saber a qué base de datos se debe conectar para ejecutar la consulta.
                  Bloque using: Al usar el bloque using, se asegura de que el objeto comando se dispose correctamente (liberando recursos) cuando se sale del bloque, incluso si ocurre una excepción.*/

                    comando.Parameters.AddWithValue("customerId", id); /*Agrega un parámetro a la consulta:
                    comando.Parameters.AddWithValue(...): Este método se utiliza para agregar un parámetro a la consulta SQL.
                    Parámetros:
                    "customerId": Este es el nombre del parámetro que se usará en la consulta SQL. Se corresponde con el marcador de posición @customerId que se incluye en la consulta SQL.
                    id: Este es el valor que se asignará al parámetro customerId. Este valor es el ID del cliente que se pasó al método ObtenerPorID y se utilizará para filtrar el resultado de la consulta.*/


                    var reader = comando.ExecuteReader(); /*Ejecuta la consulta SQL:
                    comando.ExecuteReader(): Este método se utiliza para ejecutar la consulta SQL que se preparó previamente en el objeto SqlCommand (comando).
                    Tipo de retorno: Devuelve un objeto SqlDataReader, que se asigna a la variable reader.
                    Propósito: SqlDataReader permite leer filas de datos que se devuelven de la base de datos en forma de un flujo de datos, lo que es eficiente para manejar grandes cantidades de datos. Esto significa que puedes leer los resultados de la consulta fila por fila.*/
                    Customers customers = null; /*Declara una variable para almacenar el resultado:
                    Customers customers = null;: Se declara una variable llamada customers de tipo Customers y se inicializa con null.
                    Propósito: Esta variable se utilizará más adelante para almacenar el objeto que representa el cliente recuperado de la base de datos. Inicializarla con null es útil para verificar posteriormente si se encontró un cliente o no (es decir, si customers sigue siendo null, significa que no se encontró un registro correspondiente al CustomerID buscado).*/

                    //validadmos 
                    if (reader.Read())
                    { /*Lee la siguiente fila de resultados:
                       reader.Read(): Este método se utiliza para avanzar al siguiente registro (fila) en el flujo de datos proporcionado por el SqlDataReader (reader).
                       Valor de retorno: Devuelve true si hay una fila disponible para leer; de lo contrario, devuelve false.
                       Propósito: Este if evalúa si se ha podido leer al menos una fila de resultados de la consulta SQL. Si no hay filas, significa que no se encontró un cliente con el CustomerID proporcionado.*/
                        customers = LeerDelDataReader(reader);/*Lee los datos de la fila actual:
                        LeerDelDataReader(reader): Este método se invoca para extraer los datos de la fila actual del SqlDataReader (reader).
                        Asignación: El resultado de este método se asigna a la variable customers.
                        Propósito: LeerDelDataReader es una función que generalmente convierte los datos de la fila en un objeto del tipo Customers, llenando sus propiedades con los valores correspondientes de las columnas leídas.*/
                    }
                    return customers; /*Devuelve el objeto customers:
                    return customers;: Esta línea devuelve el objeto customers desde el método ObtenerPorID.
                    Valor de retorno: Si se ha encontrado un cliente en la base de datos, se devuelve el objeto customers con sus propiedades llenas. Si no se encontró ningún cliente, customers seguirá siendo null y se devolverá como tal.*/
                }

            }
        }
        public Customers LeerDelDataReader( SqlDataReader reader)
        { /*El propósito general del método LeerDelDataReader es el siguiente:
          Leer datos del SqlDataReader: El método utilizará el objeto reader para extraer información de un registro de cliente que se ha obtenido a partir de una consulta a la base de datos.
          Crear y devolver un objeto Customers: Utilizando los datos leídos del reader, el método creará una nueva instancia de un objeto Customers, llenando sus propiedades con los valores correspondientes de la fila leída. Luego, este objeto se devolverá al llamador del método.*/

            Customers customers = new Customers(); /*Crear una nueva instancia de Customers:
            Se crea un nuevo objeto llamado customers de tipo Customers. Este objeto servirá para almacenar los datos del cliente que se leerán a continuación.*/
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"]; /*Asignar CustomerID:
Esta línea asigna el valor de la columna CustomerID del reader al atributo CustomerID del objeto customers.
Verificación de DBNull: Utiliza un operador ternario para comprobar si el valor de reader["CustomerID"] es DBNull. Si es DBNull, se asigna un espacio en blanco " "; de lo contrario, se convierte el valor a String.*/
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"]; /*Asignar CompanyName:
Similar a la línea anterior, se asigna el valor de CompanyName de la base de datos al atributo correspondiente del objeto customers, utilizando una verificación de DBNull para manejar posibles valores nulos.*/
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"]; /*Asignar ContactName:
Se asigna el valor de ContactName, comprobando primero si es DBNull.*/
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"]; /*Asignar ContactTitle:
Se asigna el valor de ContactTitle, con la misma verificación de DBNull.*/
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"]; /*Asignar Address:
Se asigna el valor de Address, verificando también si es DBNull.*/
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"]; /*Asignar City:
Se asigna el valor de City, con la verificación correspondiente.*/
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"]; /*Asignar Region:
Se asigna el valor de Region, asegurándose de que no sea DBNull.*/
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"]; /*Asignar PostalCode:
Se asigna el valor de PostalCode, usando la verificación de DBNull.
*/
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"]; /*Asignar Country:
Se asigna el valor de Country, también con la verificación de DBNull.*/
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"]; /*Asignar Phone:
Se asigna el valor de Phone, verificando si es DBNull.*/
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"]; /*Asignar Fax:
Se asigna el valor de Fax, con la correspondiente verificación de DBNull.*/
            return customers; /*Devolver el objeto customers:
Al final del método, se devuelve el objeto customers, que ahora contiene todos los datos leídos de la fila actual del SqlDataReader. Este objeto se puede utilizar en otras partes del programa.*/
        }
        //-------------
        public int InsertarCliente(Customers customer) { /*Declaración del Método:
Esta línea declara un método público llamado InsertarCliente.
Tipo de Retorno: El método devolverá un int, que normalmente representa el número de registros afectados por la operación de inserción (generalmente 1 si se inserta con éxito o 0 si no se inserta nada).
Parámetro: El método recibe un parámetro llamado customer de tipo Customers, que es el objeto que contiene la información del cliente que se desea insertar en la base de datos.*/
            using (var conexion = DataBase.GetSqlConnection()) { /*Uso de using:
Este bloque using se utiliza para asegurarse de que el recurso conexion se maneje adecuadamente y se cierre automáticamente al final del bloque, incluso si ocurre una excepción. Esto es importante para evitar fugas de recursos en la aplicación.
var conexion:
conexion es una variable que se inicializa utilizando el método GetSqlConnection() de la clase DataBase. Este método se espera que devuelva una conexión válida a la base de datos SQL.
Al usar var, el compilador infiere automáticamente el tipo de conexion, que será SqlConnection o un tipo relacionado, dependiendo de la implementación de GetSqlConnection().*/
                String insertInto = ""; /*Declaración de la Variable:
Se declara una variable llamada insertInto de tipo String y se inicializa con una cadena vacía. Esta variable se utilizará para construir la consulta SQL.*/
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n"; /*Construcción de la Consulta:
Se concatena a insertInto la parte inicial de la consulta SQL que indica que se desea insertar datos en la tabla Customers del esquema dbo. El "\n" se usa para agregar un salto de línea, lo que puede mejorar la legibilidad del código.*/
                insertInto = insertInto + "           ([CustomerID] " + "\n"; /*Especificación de Columnas:
Se añade la primera columna a la lista de columnas en las que se insertarán los valores, que es CustomerID. Los espacios antes del texto ayudan a alinear visualmente el código.*/
                insertInto = insertInto + "           ,[CompanyName] " + "\n";//
                insertInto = insertInto + "           ,[ContactName] " + "\n";//
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";//  /*Estas líneas continúan agregando las demás columnas (CompanyName, ContactName, ContactTitle, Address, City) a la lista. La estructura sigue el mismo formato, y al final se cierra la lista de columnas con un paréntesis.*/
                insertInto = insertInto + "           ,[Address] " + "\n";//
                insertInto = insertInto + "           ,[City]) " + "\n";//
                insertInto = insertInto + "     VALUES " + "\n"; /*Introducción de la Sección de Valores:
Se añade la cláusula VALUES a la consulta, que indica que a continuación se especificarán los valores que se insertarán en las columnas mencionadas anteriormente.*/
                insertInto = insertInto + "           (@CustomerID " + "\n"; /*Parámetro para el Valor:
Se agrega un marcador de posición para el valor que se insertará en la columna CustomerID. Este marcador se usará para pasar el valor real a la consulta cuando se ejecute.*/
                insertInto = insertInto + "           ,@CompanyName " + "\n";//
                insertInto = insertInto + "           ,@ContactName " + "\n";//
                insertInto = insertInto + "           ,@ContactTitle " + "\n";//  /*Continuación de los Parámetros: Estas líneas continúan agregando marcadores de posición para los valores que se insertarán en las columnas restantes.Cada marcador(@CompanyName, @ContactName, etc.) representa un parámetro que se asignará más adelante en la ejecución de la consulta.*/
                insertInto = insertInto + "           ,@Address " + "\n";//
                insertInto = insertInto + "           ,@City)";//

                using (var comando = new SqlCommand( insertInto,conexion )) {
                  int  insertados = parametrosCliente(customer, comando);
                    return insertados;
                }

            }
        }
        //-------------
        public int ActualizarCliente(Customers customer) {
            using (var conexion = DataBase.GetSqlConnection()) {
                String ActualizarCustomerPorID = "";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "UPDATE [dbo].[Customers] " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "   SET [CustomerID] = @CustomerID " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[City] = @City " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " WHERE CustomerID= @CustomerID";
                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion)) {

                    int actualizados = parametrosCliente(customer, comando);

                    return actualizados;
                }
            } 
        }

        public int parametrosCliente(Customers customer, SqlCommand comando) {
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactName);
            comando.Parameters.AddWithValue("Address", customer.Address);
            comando.Parameters.AddWithValue("City", customer.City);
            var insertados = comando.ExecuteNonQuery();
            return insertados;
        }

        public int EliminarCliente(string id) {
            using (var conexion = DataBase.GetSqlConnection() ){
                String EliminarCliente = "";
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n";
                EliminarCliente = EliminarCliente + "      WHERE CustomerID = @CustomerID";
                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion)) {
                    comando.Parameters.AddWithValue("@CustomerID", id);
                    int elimindos = comando.ExecuteNonQuery();
                    return elimindos;
                }
            }
        }
    }
}
