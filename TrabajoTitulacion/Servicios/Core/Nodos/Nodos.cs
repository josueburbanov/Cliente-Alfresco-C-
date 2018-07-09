﻿using RestSharp;
using System;
using RestSharp.Extensions;

namespace TrabajoTitulacion.Servicios.Core.Nodos
{
    class Nodos : INodos
    {
        const string URL_BASE = "http://127.0.0.1:8090/alfresco/api/-default-/public/alfresco/versions/1";
        RestClient cliente = new RestClient(URL_BASE);

        public void ObtenerContenido(string idNodo, string path)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "nodes/" + idNodo + "/content";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);            
            (cliente.DownloadData(solicitud)).SaveAs(path);            
        }

        public string ObtenerListaNodosHijos(string nodoPadre)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "nodes/"+nodoPadre+"/children";
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = cliente.Execute(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }

        public string ObtenerNodo(string idNodo)
        {
            var solicitud = new RestRequest(Method.GET);
            solicitud.Resource = "nodes/" + idNodo;
            solicitud.AddQueryParameter("alf_ticket", AutenticacionStatic.Ticket);
            IRestResponse respuesta = cliente.Execute(solicitud);
            var contenidoRespuesta = respuesta.Content;
            if (!respuesta.IsSuccessful) throw new UnauthorizedAccessException();
            return contenidoRespuesta;
        }
    }
}