namespace Ivan.Business.Rule
{
    using System;


    public class NomeDaMarcaRepetidaException : Exception
    {
        public NomeDaMarcaRepetidaException() : base("Uma marca com nome repetido foi detectada.")
        {
        }
    }
}