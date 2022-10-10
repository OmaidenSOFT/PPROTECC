using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace Tranversal
{
    class Utilcs
    {
        private static object Bitmap { get; set; }

        public static string ImageToXMLNode(System.Drawing.Image imagen) // XmlNode
        {
            MemoryStream oStream = new MemoryStream();
            XmlDocument oDom = new XmlDocument();
            System.IO.MemoryStream mResult = new System.IO.MemoryStream();
            long LenData = 0;
            byte[] Buffer;
            System.IO.BinaryReader oBinaryReader;
            XmlTextWriter oXMLTextWriter;
            System.IO.StreamReader oStreamReader;
            string StrResult;

            // Verifico si existe imagen a serializar
            if (imagen != null)
            {

                // Se graba en Stream para almacenar la imagen en formato binario
                // Se conserva el formato de la imagen
                System.Drawing.Imaging.ImageFormat imgF;
                imgF = imagen.RawFormat;
                imagen.Save(oStream, imgF);

                oStream.Position = 0;

                LenData = oStream.Length - 1;

                // Verifico la longitud de datos a serializar        
                if (LenData > 0)
                {
                    Buffer = new byte[Convert.ToInt32(LenData) + 1]; // Genero Buffer

                    // Leo los datos binarios
                    oBinaryReader = new System.IO.BinaryReader(oStream, Encoding.UTF8);
                    oBinaryReader.Read(Buffer, 0, Buffer.Length);

                    // Creo XMLTextWriter y agrego nodo con la imagen
                    oXMLTextWriter = new XmlTextWriter(mResult, Encoding.UTF8);
                    oXMLTextWriter.WriteStartDocument();
                    oXMLTextWriter.WriteStartElement("BinaryData");
                    oXMLTextWriter.WriteBase64(Buffer, 0, Buffer.Length);
                    oXMLTextWriter.WriteEndElement();
                    oXMLTextWriter.WriteEndDocument();
                    oXMLTextWriter.Flush();

                    // posiciono en 0 el resultado
                    mResult.Position = 0;

                    // Pasa el Stream a String y retorna
                    oStreamReader = new System.IO.StreamReader(mResult, Encoding.UTF8);
                    StrResult = oStreamReader.ReadToEnd();
                    oStreamReader.Close();

                    return StrResult;
                }
                else
                {
                    // En caso de no existir datos retorno el XML con formato vacio
                    oDom.LoadXml("<BinaryData/>");
                    return oDom.DocumentElement.CloneNode(true).ToString();
                }
            }
            else
            {
                // no hay imagen devuelvo el XML Vacio
                oDom.LoadXml("<BinaryData/>");
                return oDom.DocumentElement.CloneNode(true).ToString();
            }
        }

        public static XmlNode ImageToXMLNode(System.Drawing.Image imagen, string nombre)
        {
            MemoryStream oStream = new MemoryStream();
            XmlDocument oDom = new XmlDocument();
            System.IO.MemoryStream mResult = new System.IO.MemoryStream();
            long LenData = 0;
            byte[] Buffer;
            System.IO.BinaryReader oBinaryReader;
            XmlTextWriter oXMLTextWriter;
            System.IO.StreamReader oStreamReader;
            string StrResult;

            // Verifico si existe imagen a serializar
            if (imagen != null)
            {

                // Se graba en Stream para almacenar la imagen en formato binario
                // Se conserva el formato de la imagen
                System.Drawing.Imaging.ImageFormat imgF;
                imgF = imagen.RawFormat;
                imagen.Save(oStream, imgF);

                oStream.Position = 0;

                LenData = oStream.Length - 1;

                // Verifico la longitud de datos a serializar        
                if (LenData > 0)
                {
                    Buffer = new byte[Convert.ToInt32(LenData) + 1]; // Genero Buffer

                    // Leo los datos binarios
                    oBinaryReader = new System.IO.BinaryReader(oStream, Encoding.UTF8);
                    oBinaryReader.Read(Buffer, 0, Buffer.Length);

                    // Creo XMLTextWriter y agrego nodo con la imagen
                    oXMLTextWriter = new XmlTextWriter(mResult, Encoding.UTF8);
                    oXMLTextWriter.WriteStartDocument();
                    oXMLTextWriter.WriteStartElement("BinaryData");
                    oXMLTextWriter.WriteAttributeString("Nombre", nombre);
                    oXMLTextWriter.WriteBase64(Buffer, 0, Buffer.Length);
                    oXMLTextWriter.WriteEndElement();
                    oXMLTextWriter.WriteEndDocument();
                    oXMLTextWriter.Flush();

                    // posiciono en 0 el resultado
                    mResult.Position = 0;

                    // Pasa el Stream a String y retorna
                    oStreamReader = new System.IO.StreamReader(mResult, Encoding.UTF8);
                    StrResult = oStreamReader.ReadToEnd();
                    oStreamReader.Close();

                    // Agrego Nuevo Nodo con imagen
                    oDom.LoadXml(StrResult);
                    return oDom.DocumentElement;
                }
                else
                {
                    // En caso de no existir datos retorno el XML con formato vacio
                    oDom.LoadXml("<BinaryData/>");
                    return oDom.DocumentElement.CloneNode(true);
                }
            }
            else
            {
                // no hay imagen devuelvo el XML Vacio
                oDom.LoadXml("<BinaryData/>");
                return oDom.DocumentElement.CloneNode(true);
            }
        }


        // Desserializa XML y retorna la Imágen
        public static System.Drawing.Image XMLNodeToImage(System.Xml.XmlNode Nodo)
        {
            // Public Shared Function XMLNodeToImage(ByVal Nodo As Xml.XmlNode) As Image
            int IntResult = 0;
            int IntPosition = 0;
            int LenBytes = 1024 * 1024; // 1024KB - 1MB Lee bloques de 1MB
            byte[] myBytes = new byte[LenBytes - 1 + 1];
            System.IO.MemoryStream oMem = new System.IO.MemoryStream();
            System.Xml.XmlTextReader oXMLTextReader;
            bool NodeFound = false;
            // Dim oStreamReader As IO.StreamReader
            System.IO.StreamWriter oStreamWriter;
            System.IO.MemoryStream oTempMem = new System.IO.MemoryStream();
            string nombre = "";

            try
            {
                // Cargo nodo de texto en Memory Stream
                // para almacenar la imagen temporalmente en bytes
                oStreamWriter = new System.IO.StreamWriter(oTempMem, Encoding.UTF8);
                oStreamWriter.Write(Nodo.OuterXml);
                oStreamWriter.Flush();
                oTempMem.Position = 0;

                // Cargo un xmlReader con el Memory Stream para leer la imágen almacenada
                oXMLTextReader = new System.Xml.XmlTextReader(oTempMem);

                // Busco el Nodo en Binario
                while (oXMLTextReader.Read())
                {
                    if (oXMLTextReader.Name == "BinaryData")
                    {
                        NodeFound = true;
                        break;
                    }
                }

                // Verifico si se encontró el Nodo con la imagen
                if (NodeFound)
                {
                    if (oXMLTextReader.HasAttributes)
                        nombre = oXMLTextReader.GetAttribute("Nombre");

                    // Lo encontro, me muevo a la Posicion Inicial del Stream para leerlo
                    IntPosition = 0;

                    // Intento Leer
                    IntResult = oXMLTextReader.ReadBase64(myBytes, 0, LenBytes);
                    while (IntResult > 0)
                    {

                        // Escribe datos

                        oMem.Write(myBytes, 0, IntResult);

                        // Limpio el array
                        Array.Clear(myBytes, 0, LenBytes);

                        // Leo nuevamente
                        IntResult = oXMLTextReader.ReadBase64(myBytes, 0, LenBytes);
                    }
                    try
                    {
                        // Intento crear la Imagen y retornarla si no devuelvo Nothing
                        Image img;
                        img = System.Drawing.Bitmap.FromStream(oMem, true, true);
                        if ((nombre != null) && (nombre.Length > 0))
                            img.Tag = nombre;
                        return img;
                    }
                    catch (Exception ex)
                    {
                        return null/* TODO Change to default(_) if this is not a reference type */;
                    }
                }
                else
                    // No encontró el nodo de imágen
                    return null/* TODO Change to default(_) if this is not a reference type */;
            }
            catch (Exception ex)
            {
                // Ocurrio un error no contemplado Retorno Nothing    
                return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }
    }
}
