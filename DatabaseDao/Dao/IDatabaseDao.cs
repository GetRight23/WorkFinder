using System.Collections.Generic;

namespace DatabaseDao
{
	public interface IDatabaseDao<Type>
	{
		Type selectEntityById(int id);
		List<Type> selectEntities();
		List<Type> selectEntitiesByIds(List<int> ids);
		
		bool deleteEntityById(int id);
		bool deleteEntitiesByIds(List<int> ids);

		int insertEntity(Type entity);
		List<int> insertEntities(List<Type> entities);

		bool updateEntity(Type entity);
		bool updateEntities(List<Type> entities);
	}
}
