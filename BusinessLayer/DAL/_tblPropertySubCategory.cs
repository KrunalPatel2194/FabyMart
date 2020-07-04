
/*
'===============================================================================
'  Generated From - CSharp_dOOdads_BusinessEntity.vbgen
' 
'  ** IMPORTANT  ** 
'  How to Generate your stored procedures:
' 
'  SQL        = SQL_StoredProcs.vbgen
'  ACCESS     = Access_StoredProcs.vbgen
'  ORACLE     = Oracle_StoredProcs.vbgen
'  FIREBIRD   = FirebirdStoredProcs.vbgen
'  POSTGRESQL = PostgreSQL_StoredProcs.vbgen
'
'  The supporting base class SqlClientEntity is in the Architecture directory in "dOOdads".
'  
'  This object is 'abstract' which means you need to inherit from it to be able
'  to instantiate it.  This is very easilly done. You can override properties and
'  methods in your derived class, this allows you to regenerate this class at any
'  time and not worry about overwriting custom code. 
'
'  NEVER EDIT THIS FILE.
'
'  public class YourObject :  _YourObject
'  {
'
'  }
'
'===============================================================================
*/

// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;

using MyGeneration.dOOdads;

namespace BusinessLayer
{
	public abstract class _tblPropertySubCategory : SqlClientEntity
	{
		public _tblPropertySubCategory()
		{
			this.QuerySource = "tblPropertySubCategory";
			this.MappingName = "tblPropertySubCategory";

		}	

		//=================================================================
		//  public Overrides void AddNew()
		//=================================================================
		//
		//=================================================================
		public override void AddNew()
		{
			base.AddNew();
			
		}
		
		
		public override void FlushData()
		{
			this._whereClause = null;
			this._aggregateClause = null;
			base.FlushData();
		}
		
		//=================================================================
		//  	public Function LoadAll() As Boolean
		//=================================================================
		//  Loads all of the records in the database, and sets the currentRow to the first row
		//=================================================================
		public bool LoadAll() 
		{
			ListDictionary parameters = null;
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblPropertySubCategoryLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int AppPropertySubCategoryID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.AppPropertySubCategoryID, AppPropertySubCategoryID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblPropertySubCategoryLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter AppPropertySubCategoryID
			{
				get
				{
					return new SqlParameter("@AppPropertySubCategoryID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppPropertyID
			{
				get
				{
					return new SqlParameter("@AppPropertyID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppSubCategoryID
			{
				get
				{
					return new SqlParameter("@AppSubCategoryID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppIsActive
			{
				get
				{
					return new SqlParameter("@AppIsActive", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter AppDisplayOrder
			{
				get
				{
					return new SqlParameter("@AppDisplayOrder", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppCreatedBy
			{
				get
				{
					return new SqlParameter("@AppCreatedBy", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppCreatedDate
			{
				get
				{
					return new SqlParameter("@AppCreatedDate", SqlDbType.DateTime, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string AppPropertySubCategoryID = "appPropertySubCategoryID";
            public const string AppPropertyID = "appPropertyID";
            public const string AppSubCategoryID = "appSubCategoryID";
            public const string AppIsActive = "appIsActive";
            public const string AppDisplayOrder = "appDisplayOrder";
            public const string AppCreatedBy = "appCreatedBy";
            public const string AppCreatedDate = "appCreatedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppPropertySubCategoryID] = _tblPropertySubCategory.PropertyNames.AppPropertySubCategoryID;
					ht[AppPropertyID] = _tblPropertySubCategory.PropertyNames.AppPropertyID;
					ht[AppSubCategoryID] = _tblPropertySubCategory.PropertyNames.AppSubCategoryID;
					ht[AppIsActive] = _tblPropertySubCategory.PropertyNames.AppIsActive;
					ht[AppDisplayOrder] = _tblPropertySubCategory.PropertyNames.AppDisplayOrder;
					ht[AppCreatedBy] = _tblPropertySubCategory.PropertyNames.AppCreatedBy;
					ht[AppCreatedDate] = _tblPropertySubCategory.PropertyNames.AppCreatedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string AppPropertySubCategoryID = "AppPropertySubCategoryID";
            public const string AppPropertyID = "AppPropertyID";
            public const string AppSubCategoryID = "AppSubCategoryID";
            public const string AppIsActive = "AppIsActive";
            public const string AppDisplayOrder = "AppDisplayOrder";
            public const string AppCreatedBy = "AppCreatedBy";
            public const string AppCreatedDate = "AppCreatedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppPropertySubCategoryID] = _tblPropertySubCategory.ColumnNames.AppPropertySubCategoryID;
					ht[AppPropertyID] = _tblPropertySubCategory.ColumnNames.AppPropertyID;
					ht[AppSubCategoryID] = _tblPropertySubCategory.ColumnNames.AppSubCategoryID;
					ht[AppIsActive] = _tblPropertySubCategory.ColumnNames.AppIsActive;
					ht[AppDisplayOrder] = _tblPropertySubCategory.ColumnNames.AppDisplayOrder;
					ht[AppCreatedBy] = _tblPropertySubCategory.ColumnNames.AppCreatedBy;
					ht[AppCreatedDate] = _tblPropertySubCategory.ColumnNames.AppCreatedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string AppPropertySubCategoryID = "s_AppPropertySubCategoryID";
            public const string AppPropertyID = "s_AppPropertyID";
            public const string AppSubCategoryID = "s_AppSubCategoryID";
            public const string AppIsActive = "s_AppIsActive";
            public const string AppDisplayOrder = "s_AppDisplayOrder";
            public const string AppCreatedBy = "s_AppCreatedBy";
            public const string AppCreatedDate = "s_AppCreatedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int AppPropertySubCategoryID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppPropertySubCategoryID);
			}
			set
	        {
				base.Setint(ColumnNames.AppPropertySubCategoryID, value);
			}
		}

		public virtual int AppPropertyID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppPropertyID);
			}
			set
	        {
				base.Setint(ColumnNames.AppPropertyID, value);
			}
		}

		public virtual int AppSubCategoryID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppSubCategoryID);
			}
			set
	        {
				base.Setint(ColumnNames.AppSubCategoryID, value);
			}
		}

		public virtual bool AppIsActive
	    {
			get
	        {
				return base.Getbool(ColumnNames.AppIsActive);
			}
			set
	        {
				base.Setbool(ColumnNames.AppIsActive, value);
			}
		}

		public virtual int AppDisplayOrder
	    {
			get
	        {
				return base.Getint(ColumnNames.AppDisplayOrder);
			}
			set
	        {
				base.Setint(ColumnNames.AppDisplayOrder, value);
			}
		}

		public virtual int AppCreatedBy
	    {
			get
	        {
				return base.Getint(ColumnNames.AppCreatedBy);
			}
			set
	        {
				base.Setint(ColumnNames.AppCreatedBy, value);
			}
		}

		public virtual DateTime AppCreatedDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.AppCreatedDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.AppCreatedDate, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_AppPropertySubCategoryID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppPropertySubCategoryID) ? string.Empty : base.GetintAsString(ColumnNames.AppPropertySubCategoryID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppPropertySubCategoryID);
				else
					this.AppPropertySubCategoryID = base.SetintAsString(ColumnNames.AppPropertySubCategoryID, value);
			}
		}

		public virtual string s_AppPropertyID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppPropertyID) ? string.Empty : base.GetintAsString(ColumnNames.AppPropertyID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppPropertyID);
				else
					this.AppPropertyID = base.SetintAsString(ColumnNames.AppPropertyID, value);
			}
		}

		public virtual string s_AppSubCategoryID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppSubCategoryID) ? string.Empty : base.GetintAsString(ColumnNames.AppSubCategoryID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppSubCategoryID);
				else
					this.AppSubCategoryID = base.SetintAsString(ColumnNames.AppSubCategoryID, value);
			}
		}

		public virtual string s_AppIsActive
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppIsActive) ? string.Empty : base.GetboolAsString(ColumnNames.AppIsActive);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppIsActive);
				else
					this.AppIsActive = base.SetboolAsString(ColumnNames.AppIsActive, value);
			}
		}

		public virtual string s_AppDisplayOrder
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppDisplayOrder) ? string.Empty : base.GetintAsString(ColumnNames.AppDisplayOrder);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppDisplayOrder);
				else
					this.AppDisplayOrder = base.SetintAsString(ColumnNames.AppDisplayOrder, value);
			}
		}

		public virtual string s_AppCreatedBy
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppCreatedBy) ? string.Empty : base.GetintAsString(ColumnNames.AppCreatedBy);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppCreatedBy);
				else
					this.AppCreatedBy = base.SetintAsString(ColumnNames.AppCreatedBy, value);
			}
		}

		public virtual string s_AppCreatedDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppCreatedDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.AppCreatedDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppCreatedDate);
				else
					this.AppCreatedDate = base.SetDateTimeAsString(ColumnNames.AppCreatedDate, value);
			}
		}


		#endregion		
	
		#region Where Clause
		public class WhereClause
		{
			public WhereClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffWhereParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffWhereParameter(this);
					}

					return _tearOff;
				}
			}

			#region WhereParameter TearOff's
			public class TearOffWhereParameter
			{
				public TearOffWhereParameter(WhereClause clause)
				{
					this._clause = clause;
				}
				
				
				public WhereParameter AppPropertySubCategoryID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppPropertySubCategoryID, Parameters.AppPropertySubCategoryID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppPropertyID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppPropertyID, Parameters.AppPropertyID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppSubCategoryID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppSubCategoryID, Parameters.AppSubCategoryID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppIsActive
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppIsActive, Parameters.AppIsActive);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppDisplayOrder
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppDisplayOrder, Parameters.AppDisplayOrder);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppCreatedBy
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppCreatedBy, Parameters.AppCreatedBy);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppCreatedDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppCreatedDate, Parameters.AppCreatedDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter AppPropertySubCategoryID
		    {
				get
		        {
					if(_AppPropertySubCategoryID_W == null)
	        	    {
						_AppPropertySubCategoryID_W = TearOff.AppPropertySubCategoryID;
					}
					return _AppPropertySubCategoryID_W;
				}
			}

			public WhereParameter AppPropertyID
		    {
				get
		        {
					if(_AppPropertyID_W == null)
	        	    {
						_AppPropertyID_W = TearOff.AppPropertyID;
					}
					return _AppPropertyID_W;
				}
			}

			public WhereParameter AppSubCategoryID
		    {
				get
		        {
					if(_AppSubCategoryID_W == null)
	        	    {
						_AppSubCategoryID_W = TearOff.AppSubCategoryID;
					}
					return _AppSubCategoryID_W;
				}
			}

			public WhereParameter AppIsActive
		    {
				get
		        {
					if(_AppIsActive_W == null)
	        	    {
						_AppIsActive_W = TearOff.AppIsActive;
					}
					return _AppIsActive_W;
				}
			}

			public WhereParameter AppDisplayOrder
		    {
				get
		        {
					if(_AppDisplayOrder_W == null)
	        	    {
						_AppDisplayOrder_W = TearOff.AppDisplayOrder;
					}
					return _AppDisplayOrder_W;
				}
			}

			public WhereParameter AppCreatedBy
		    {
				get
		        {
					if(_AppCreatedBy_W == null)
	        	    {
						_AppCreatedBy_W = TearOff.AppCreatedBy;
					}
					return _AppCreatedBy_W;
				}
			}

			public WhereParameter AppCreatedDate
		    {
				get
		        {
					if(_AppCreatedDate_W == null)
	        	    {
						_AppCreatedDate_W = TearOff.AppCreatedDate;
					}
					return _AppCreatedDate_W;
				}
			}

			private WhereParameter _AppPropertySubCategoryID_W = null;
			private WhereParameter _AppPropertyID_W = null;
			private WhereParameter _AppSubCategoryID_W = null;
			private WhereParameter _AppIsActive_W = null;
			private WhereParameter _AppDisplayOrder_W = null;
			private WhereParameter _AppCreatedBy_W = null;
			private WhereParameter _AppCreatedDate_W = null;

			public void WhereClauseReset()
			{
				_AppPropertySubCategoryID_W = null;
				_AppPropertyID_W = null;
				_AppSubCategoryID_W = null;
				_AppIsActive_W = null;
				_AppDisplayOrder_W = null;
				_AppCreatedBy_W = null;
				_AppCreatedDate_W = null;

				this._entity.Query.FlushWhereParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffWhereParameter _tearOff;
			
		}
	
		public WhereClause Where
		{
			get
			{
				if(_whereClause == null)
				{
					_whereClause = new WhereClause(this);
				}
		
				return _whereClause;
			}
		}
		
		private WhereClause _whereClause = null;	
		#endregion
	
		#region Aggregate Clause
		public class AggregateClause
		{
			public AggregateClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffAggregateParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffAggregateParameter(this);
					}

					return _tearOff;
				}
			}

			#region AggregateParameter TearOff's
			public class TearOffAggregateParameter
			{
				public TearOffAggregateParameter(AggregateClause clause)
				{
					this._clause = clause;
				}
				
				
				public AggregateParameter AppPropertySubCategoryID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppPropertySubCategoryID, Parameters.AppPropertySubCategoryID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppPropertyID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppPropertyID, Parameters.AppPropertyID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppSubCategoryID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppSubCategoryID, Parameters.AppSubCategoryID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppIsActive
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppIsActive, Parameters.AppIsActive);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppDisplayOrder
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppDisplayOrder, Parameters.AppDisplayOrder);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppCreatedBy
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppCreatedBy, Parameters.AppCreatedBy);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppCreatedDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppCreatedDate, Parameters.AppCreatedDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter AppPropertySubCategoryID
		    {
				get
		        {
					if(_AppPropertySubCategoryID_W == null)
	        	    {
						_AppPropertySubCategoryID_W = TearOff.AppPropertySubCategoryID;
					}
					return _AppPropertySubCategoryID_W;
				}
			}

			public AggregateParameter AppPropertyID
		    {
				get
		        {
					if(_AppPropertyID_W == null)
	        	    {
						_AppPropertyID_W = TearOff.AppPropertyID;
					}
					return _AppPropertyID_W;
				}
			}

			public AggregateParameter AppSubCategoryID
		    {
				get
		        {
					if(_AppSubCategoryID_W == null)
	        	    {
						_AppSubCategoryID_W = TearOff.AppSubCategoryID;
					}
					return _AppSubCategoryID_W;
				}
			}

			public AggregateParameter AppIsActive
		    {
				get
		        {
					if(_AppIsActive_W == null)
	        	    {
						_AppIsActive_W = TearOff.AppIsActive;
					}
					return _AppIsActive_W;
				}
			}

			public AggregateParameter AppDisplayOrder
		    {
				get
		        {
					if(_AppDisplayOrder_W == null)
	        	    {
						_AppDisplayOrder_W = TearOff.AppDisplayOrder;
					}
					return _AppDisplayOrder_W;
				}
			}

			public AggregateParameter AppCreatedBy
		    {
				get
		        {
					if(_AppCreatedBy_W == null)
	        	    {
						_AppCreatedBy_W = TearOff.AppCreatedBy;
					}
					return _AppCreatedBy_W;
				}
			}

			public AggregateParameter AppCreatedDate
		    {
				get
		        {
					if(_AppCreatedDate_W == null)
	        	    {
						_AppCreatedDate_W = TearOff.AppCreatedDate;
					}
					return _AppCreatedDate_W;
				}
			}

			private AggregateParameter _AppPropertySubCategoryID_W = null;
			private AggregateParameter _AppPropertyID_W = null;
			private AggregateParameter _AppSubCategoryID_W = null;
			private AggregateParameter _AppIsActive_W = null;
			private AggregateParameter _AppDisplayOrder_W = null;
			private AggregateParameter _AppCreatedBy_W = null;
			private AggregateParameter _AppCreatedDate_W = null;

			public void AggregateClauseReset()
			{
				_AppPropertySubCategoryID_W = null;
				_AppPropertyID_W = null;
				_AppSubCategoryID_W = null;
				_AppIsActive_W = null;
				_AppDisplayOrder_W = null;
				_AppCreatedBy_W = null;
				_AppCreatedDate_W = null;

				this._entity.Query.FlushAggregateParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffAggregateParameter _tearOff;
			
		}
	
		public AggregateClause Aggregate
		{
			get
			{
				if(_aggregateClause == null)
				{
					_aggregateClause = new AggregateClause(this);
				}
		
				return _aggregateClause;
			}
		}
		
		private AggregateClause _aggregateClause = null;	
		#endregion
	
		protected override IDbCommand GetInsertCommand() 
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblPropertySubCategoryInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.AppPropertySubCategoryID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblPropertySubCategoryUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblPropertySubCategoryDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.AppPropertySubCategoryID);
			p.SourceColumn = ColumnNames.AppPropertySubCategoryID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.AppPropertySubCategoryID);
			p.SourceColumn = ColumnNames.AppPropertySubCategoryID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppPropertyID);
			p.SourceColumn = ColumnNames.AppPropertyID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppSubCategoryID);
			p.SourceColumn = ColumnNames.AppSubCategoryID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppIsActive);
			p.SourceColumn = ColumnNames.AppIsActive;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppDisplayOrder);
			p.SourceColumn = ColumnNames.AppDisplayOrder;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppCreatedBy);
			p.SourceColumn = ColumnNames.AppCreatedBy;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppCreatedDate);
			p.SourceColumn = ColumnNames.AppCreatedDate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}