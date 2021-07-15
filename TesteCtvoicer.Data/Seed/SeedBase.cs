using System;
using System.Collections.Generic;

namespace TesteCtvoicer.Data.Seed
{
	public abstract class SeedBase
	{
		public abstract void Executar(FrotaContext context);

		public virtual List<Type> ListarDependencias() => null;
	}
}
