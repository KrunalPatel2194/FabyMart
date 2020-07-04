
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
	public abstract class _tblSize : SqlClientEntity
	{
		public _tblSize()
		{
			this.QuerySource = "tblSize";
			this.MappingName = "tblSize";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSizeLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int AppSizeID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.AppSizeID, AppSizeID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSizeLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter AppSizeID
			{
				get
				{
					return new SqlParameter("@AppSizeID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppSize
			{
				get
				{
					return new SqlParameter("@AppSize", SqlDbType.VarChar, 50);
				}
			}
			
			public static SqlParameter AppIsActive
			{
				get
				{
					return new SqlParameter("@AppIsActive", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter AppIsDefault
			{
				get
				{
					return new SqlParameter("@AppIsDefault", SqlDbType.Bit, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string AppSizeID = "appSizeID";
            public const string AppSize = "appSize";
            public const string AppIsActive = "appIsActive";
            public const string AppIsDefault = "appIsDefault";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppSizeID] = _tblSize.PropertyNames.AppSizeID;
					ht[AppSize] = _tblSize.PropertyNames.AppSize;
					ht[AppIsActive] = _tblSize.PropertyNames.AppIsActive;
					ht[AppIsDefault] = _tblSize.PropertyNames.AppIsDefault;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string AppSizeID = "AppSizeID";
            public const string AppSize = "AppSize";
            public const string AppIsActive = "AppIsActive";
            public const string AppIsDefault = "AppIsDefault";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppSizeID] = _tblSize.ColumnNames.AppSizeID;
					ht[AppSize] = _tblSize.ColumnNames.AppSize;
					ht[AppIsActive] = _tblSize.ColumnNames.AppIsActive;
					ht[AppIsDefault] = _tblSize.ColumnNames.AppIsDefault;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string AppSizeID = "s_AppSizeID";
            public const string AppSize = "s_AppSize";
            public const string AppIsActive = "s_AppIsActive";
            public const string AppIsDefault = "s_AppIsDefault";

		}
		#endregion		
		
		#region Properties
	
		public virtual int AppSizeID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppSizeID);
			}
			set
	        {
				base.Setint(ColumnNames.AppSizeID, value);
			}
		}

		public virtual string AppSize
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppSize);
			}
			set
	        {
				base.Setstring(ColumnNames.AppSize, value);
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

		public virtual bool AppIsDefault
	    {
			get
	        {
				return base.Getbool(ColumnNames.AppIsDefault);
			}
			set
	        {
				base.Setbool(ColumnNames.AppIsDefault, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_AppSizeID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppSizeID) ? string.Empty : base.GetintAsString(ColumnNames.AppSizeID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppSizeID);
				else
					this.AppSizeID = base.SetintAsString(ColumnNames.AppSizeID, value);
			}
		}

		public virtual string s_AppSize
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppSize) ? string.Empty : base.GetstringAsString(ColumnNames.AppSize);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppSize);
				else
					this.AppSize = base.SetstringAsString(ColumnNames.AppSize, value);
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

		public virtual string s_AppIsDefault
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppIsDefault) ? string.Empty : base.GetboolAsString(ColumnNames.AppIsDefault);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppIsDefault);
				else
					this.AppIsDefault = base.SetboolAsString(ColumnNames.AppIsDefault, value);
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
				
				
				public WhereParameter AppSizeID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppSizeID, Parameters.AppSizeID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppSize
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppSize, Parameters.AppSize);
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

				public WhereParameter AppIsDefault
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppIsDefault, Parameters.AppIsDefault);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter AppSizeID
		    {
				get
		        {
					if(_AppSizeID_W == null)
	        	    {
						_AppSizeID_W = TearOff.AppSizeID;
					}
					return _AppSizeID_W;
				}
			}

			public WhereParameter AppSize
		    {
				get
		        {
					if(_AppSize_W == null)
	        	    {
						_AppSize_W = TearOff.AppSize;
					}
					return _AppSize_W;
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

			public WhereParameter AppIsDefault
		    {
				get
		        {
					if(_AppIsDefault_W == null)
	        	    {
						_AppIsDefault_W = TearOff.AppIsDefault;
					}
					return _AppIsDefault_W;
				}
			}

			private WhereParameter _AppSizeID_W = null;
			private WhereParameter _AppSize_W = null;
			private WhereParameter _AppIsActive_W = null;
			private WhereParameter _AppIsDefault_W = null;

			public void WhereClauseReset()
			{
				_AppSizeID_W = null;
				_AppSize_W = null;
				_AppIsActive_W = null;
				_AppIsDefault_W = null;

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
				
				
				public AggregateParameter AppSizeID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppSizeID, Parameters.AppSizeID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppSize
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppSize, Parameters.AppSize);
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

				public AggregateParameter AppIsDefault
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppIsDefault, Parameters.AppIsDefault);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter AppSizeID
		    {
				get
		        {
					if(_AppSizeID_W == null)
	        	    {
						_AppSizeID_W = TearOff.AppSizeID;
					}
					return _AppSizeID_W;
				}
			}

			public AggregateParameter AppSize
		    {
				get
		        {
					if(_AppSize_W == null)
	        	    {
						_AppSize_W = TearOff.AppSize;
					}
					return _AppSize_W;
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

			public AggregateParameter AppIsDefault
		    {
				get
		        {
					if(_AppIsDefault_W == null)
	        	    {
						_AppIsDefault_W = TearOff.AppIsDefault;
					}
					return _AppIsDefault_W;
				}
			}

			private AggregateParameter _AppSizeID_W = null;
			private AggregateParameter _AppSize_W = null;
			private AggregateParameter _AppIsActive_W = null;
			private AggregateParameter _AppIsDefault_W = null;

			public void AggregateClauseReset()
			{
				_AppSizeID_W = null;
				_AppSize_W = null;
				_AppIsActive_W = null;
				_AppIsDefault_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSizeInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.AppSizeID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSizeUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSizeDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.AppSizeID);
			p.SourceColumn = ColumnNames.AppSizeID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.AppSizeID);
			p.SourceColumn = ColumnNames.AppSizeID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppSize);
			p.SourceColumn = ColumnNames.AppSize;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppIsActive);
			p.SourceColumn = ColumnNames.AppIsActive;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppIsDefault);
			p.SourceColumn = ColumnNames.AppIsDefault;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
