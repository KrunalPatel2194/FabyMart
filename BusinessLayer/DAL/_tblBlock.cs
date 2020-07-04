
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
	public abstract class _tblBlock : SqlClientEntity
	{
		public _tblBlock()
		{
			this.QuerySource = "tblBlock";
			this.MappingName = "tblBlock";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblBlockLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int AppBlockId)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.AppBlockId, AppBlockId);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblBlockLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter AppBlockId
			{
				get
				{
					return new SqlParameter("@AppBlockId", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppBlockName
			{
				get
				{
					return new SqlParameter("@AppBlockName", SqlDbType.NVarChar, 100);
				}
			}
			
			public static SqlParameter AppIsShowContent
			{
				get
				{
					return new SqlParameter("@AppIsShowContent", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter AppContent
			{
				get
				{
					return new SqlParameter("@AppContent", SqlDbType.Text, 2147483647);
				}
			}
			
			public static SqlParameter AppControlId
			{
				get
				{
					return new SqlParameter("@AppControlId", SqlDbType.NVarChar, 50);
				}
			}
			
			public static SqlParameter AppCreatedDate
			{
				get
				{
					return new SqlParameter("@AppCreatedDate", SqlDbType.DateTime, 0);
				}
			}
			
			public static SqlParameter AppCreatedBy
			{
				get
				{
					return new SqlParameter("@AppCreatedBy", SqlDbType.Int, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string AppBlockId = "appBlockId";
            public const string AppBlockName = "appBlockName";
            public const string AppIsShowContent = "appIsShowContent";
            public const string AppContent = "appContent";
            public const string AppControlId = "appControlId";
            public const string AppCreatedDate = "appCreatedDate";
            public const string AppCreatedBy = "appCreatedBy";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppBlockId] = _tblBlock.PropertyNames.AppBlockId;
					ht[AppBlockName] = _tblBlock.PropertyNames.AppBlockName;
					ht[AppIsShowContent] = _tblBlock.PropertyNames.AppIsShowContent;
					ht[AppContent] = _tblBlock.PropertyNames.AppContent;
					ht[AppControlId] = _tblBlock.PropertyNames.AppControlId;
					ht[AppCreatedDate] = _tblBlock.PropertyNames.AppCreatedDate;
					ht[AppCreatedBy] = _tblBlock.PropertyNames.AppCreatedBy;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string AppBlockId = "AppBlockId";
            public const string AppBlockName = "AppBlockName";
            public const string AppIsShowContent = "AppIsShowContent";
            public const string AppContent = "AppContent";
            public const string AppControlId = "AppControlId";
            public const string AppCreatedDate = "AppCreatedDate";
            public const string AppCreatedBy = "AppCreatedBy";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppBlockId] = _tblBlock.ColumnNames.AppBlockId;
					ht[AppBlockName] = _tblBlock.ColumnNames.AppBlockName;
					ht[AppIsShowContent] = _tblBlock.ColumnNames.AppIsShowContent;
					ht[AppContent] = _tblBlock.ColumnNames.AppContent;
					ht[AppControlId] = _tblBlock.ColumnNames.AppControlId;
					ht[AppCreatedDate] = _tblBlock.ColumnNames.AppCreatedDate;
					ht[AppCreatedBy] = _tblBlock.ColumnNames.AppCreatedBy;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string AppBlockId = "s_AppBlockId";
            public const string AppBlockName = "s_AppBlockName";
            public const string AppIsShowContent = "s_AppIsShowContent";
            public const string AppContent = "s_AppContent";
            public const string AppControlId = "s_AppControlId";
            public const string AppCreatedDate = "s_AppCreatedDate";
            public const string AppCreatedBy = "s_AppCreatedBy";

		}
		#endregion		
		
		#region Properties
	
		public virtual int AppBlockId
	    {
			get
	        {
				return base.Getint(ColumnNames.AppBlockId);
			}
			set
	        {
				base.Setint(ColumnNames.AppBlockId, value);
			}
		}

		public virtual string AppBlockName
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppBlockName);
			}
			set
	        {
				base.Setstring(ColumnNames.AppBlockName, value);
			}
		}

		public virtual bool AppIsShowContent
	    {
			get
	        {
				return base.Getbool(ColumnNames.AppIsShowContent);
			}
			set
	        {
				base.Setbool(ColumnNames.AppIsShowContent, value);
			}
		}

		public virtual string AppContent
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppContent);
			}
			set
	        {
				base.Setstring(ColumnNames.AppContent, value);
			}
		}

		public virtual string AppControlId
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppControlId);
			}
			set
	        {
				base.Setstring(ColumnNames.AppControlId, value);
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


		#endregion
		
		#region String Properties
	
		public virtual string s_AppBlockId
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppBlockId) ? string.Empty : base.GetintAsString(ColumnNames.AppBlockId);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppBlockId);
				else
					this.AppBlockId = base.SetintAsString(ColumnNames.AppBlockId, value);
			}
		}

		public virtual string s_AppBlockName
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppBlockName) ? string.Empty : base.GetstringAsString(ColumnNames.AppBlockName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppBlockName);
				else
					this.AppBlockName = base.SetstringAsString(ColumnNames.AppBlockName, value);
			}
		}

		public virtual string s_AppIsShowContent
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppIsShowContent) ? string.Empty : base.GetboolAsString(ColumnNames.AppIsShowContent);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppIsShowContent);
				else
					this.AppIsShowContent = base.SetboolAsString(ColumnNames.AppIsShowContent, value);
			}
		}

		public virtual string s_AppContent
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppContent) ? string.Empty : base.GetstringAsString(ColumnNames.AppContent);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppContent);
				else
					this.AppContent = base.SetstringAsString(ColumnNames.AppContent, value);
			}
		}

		public virtual string s_AppControlId
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppControlId) ? string.Empty : base.GetstringAsString(ColumnNames.AppControlId);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppControlId);
				else
					this.AppControlId = base.SetstringAsString(ColumnNames.AppControlId, value);
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
				
				
				public WhereParameter AppBlockId
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppBlockId, Parameters.AppBlockId);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppBlockName
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppBlockName, Parameters.AppBlockName);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppIsShowContent
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppIsShowContent, Parameters.AppIsShowContent);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppContent
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppContent, Parameters.AppContent);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppControlId
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppControlId, Parameters.AppControlId);
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

				public WhereParameter AppCreatedBy
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppCreatedBy, Parameters.AppCreatedBy);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter AppBlockId
		    {
				get
		        {
					if(_AppBlockId_W == null)
	        	    {
						_AppBlockId_W = TearOff.AppBlockId;
					}
					return _AppBlockId_W;
				}
			}

			public WhereParameter AppBlockName
		    {
				get
		        {
					if(_AppBlockName_W == null)
	        	    {
						_AppBlockName_W = TearOff.AppBlockName;
					}
					return _AppBlockName_W;
				}
			}

			public WhereParameter AppIsShowContent
		    {
				get
		        {
					if(_AppIsShowContent_W == null)
	        	    {
						_AppIsShowContent_W = TearOff.AppIsShowContent;
					}
					return _AppIsShowContent_W;
				}
			}

			public WhereParameter AppContent
		    {
				get
		        {
					if(_AppContent_W == null)
	        	    {
						_AppContent_W = TearOff.AppContent;
					}
					return _AppContent_W;
				}
			}

			public WhereParameter AppControlId
		    {
				get
		        {
					if(_AppControlId_W == null)
	        	    {
						_AppControlId_W = TearOff.AppControlId;
					}
					return _AppControlId_W;
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

			private WhereParameter _AppBlockId_W = null;
			private WhereParameter _AppBlockName_W = null;
			private WhereParameter _AppIsShowContent_W = null;
			private WhereParameter _AppContent_W = null;
			private WhereParameter _AppControlId_W = null;
			private WhereParameter _AppCreatedDate_W = null;
			private WhereParameter _AppCreatedBy_W = null;

			public void WhereClauseReset()
			{
				_AppBlockId_W = null;
				_AppBlockName_W = null;
				_AppIsShowContent_W = null;
				_AppContent_W = null;
				_AppControlId_W = null;
				_AppCreatedDate_W = null;
				_AppCreatedBy_W = null;

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
				
				
				public AggregateParameter AppBlockId
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppBlockId, Parameters.AppBlockId);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppBlockName
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppBlockName, Parameters.AppBlockName);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppIsShowContent
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppIsShowContent, Parameters.AppIsShowContent);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppContent
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppContent, Parameters.AppContent);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppControlId
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppControlId, Parameters.AppControlId);
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

				public AggregateParameter AppCreatedBy
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppCreatedBy, Parameters.AppCreatedBy);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter AppBlockId
		    {
				get
		        {
					if(_AppBlockId_W == null)
	        	    {
						_AppBlockId_W = TearOff.AppBlockId;
					}
					return _AppBlockId_W;
				}
			}

			public AggregateParameter AppBlockName
		    {
				get
		        {
					if(_AppBlockName_W == null)
	        	    {
						_AppBlockName_W = TearOff.AppBlockName;
					}
					return _AppBlockName_W;
				}
			}

			public AggregateParameter AppIsShowContent
		    {
				get
		        {
					if(_AppIsShowContent_W == null)
	        	    {
						_AppIsShowContent_W = TearOff.AppIsShowContent;
					}
					return _AppIsShowContent_W;
				}
			}

			public AggregateParameter AppContent
		    {
				get
		        {
					if(_AppContent_W == null)
	        	    {
						_AppContent_W = TearOff.AppContent;
					}
					return _AppContent_W;
				}
			}

			public AggregateParameter AppControlId
		    {
				get
		        {
					if(_AppControlId_W == null)
	        	    {
						_AppControlId_W = TearOff.AppControlId;
					}
					return _AppControlId_W;
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

			private AggregateParameter _AppBlockId_W = null;
			private AggregateParameter _AppBlockName_W = null;
			private AggregateParameter _AppIsShowContent_W = null;
			private AggregateParameter _AppContent_W = null;
			private AggregateParameter _AppControlId_W = null;
			private AggregateParameter _AppCreatedDate_W = null;
			private AggregateParameter _AppCreatedBy_W = null;

			public void AggregateClauseReset()
			{
				_AppBlockId_W = null;
				_AppBlockName_W = null;
				_AppIsShowContent_W = null;
				_AppContent_W = null;
				_AppControlId_W = null;
				_AppCreatedDate_W = null;
				_AppCreatedBy_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblBlockInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.AppBlockId.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblBlockUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblBlockDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.AppBlockId);
			p.SourceColumn = ColumnNames.AppBlockId;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.AppBlockId);
			p.SourceColumn = ColumnNames.AppBlockId;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppBlockName);
			p.SourceColumn = ColumnNames.AppBlockName;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppIsShowContent);
			p.SourceColumn = ColumnNames.AppIsShowContent;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppContent);
			p.SourceColumn = ColumnNames.AppContent;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppControlId);
			p.SourceColumn = ColumnNames.AppControlId;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppCreatedDate);
			p.SourceColumn = ColumnNames.AppCreatedDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppCreatedBy);
			p.SourceColumn = ColumnNames.AppCreatedBy;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
