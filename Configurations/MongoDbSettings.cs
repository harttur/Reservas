namespace Reservas.Configurations
{
	/// <summary>
	/// Representa as configurações necessárias para a conexão com o banco de dados MongoDB.
	/// </summary>
	public class MongoDbSettings
	{
		/// <summary>
		/// String de conexão para acessar o servidor MongoDB.
		/// </summary>
		public string ConnectionString { get; set; }

		/// <summary>
		/// Nome do banco de dados a ser utilizado.
		/// </summary>
		public string DatabaseName { get; set; }
	}
}
