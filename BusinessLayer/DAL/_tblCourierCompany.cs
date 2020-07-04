
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
	public abstract class _tblCourierCompany : SqlClientEntity
	{
		public _tblCourierCompany()
		{
			this.QuerySource = "tblCourierCompany";
			this.MappingName = "tblCourierCompany";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblCourierCompanyLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int AppCourierCompanyID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.AppCourierCompanyID, AppCourierCompanyID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblCourierCompanyLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter AppCourierCompanyID
			{
				get
				{
					return new SqlParameter("@AppCourierCompanyID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppCourierCompany
			{
				get
				{
					return new SqlParameter("@AppCourierCompany", SqlDbType.VarChar, 50);
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
			
			public static SqlParameter AppWebsite
			{
				get
				{
					return new SqlParameter("@AppWebsite", SqlDbType.VarChar, 150);
				}
			}
			
			public static SqlParameter AppContactNo
			{
				get
				{
					return new SqlParameter("@AppContactNo", SqlDbType.VarChar, 20);
				}
			}
			
			public static SqlParameter AppEmail
			{
				get
				{
					return new SqlParameter("@AppEmail", SqlDbType.VarChar, 100);
				}
			}
			
			public static SqlParameter AppCODRate
			{
				get
				{
					return new SqlParameter("@AppCODRate", SqlDbType.Decimal, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string AppCourierCompanyID = "appCourierCompanyID";
            public const string AppCourierCompany = "appCourierCompany";
            public const string AppIsActive = "appIsActive";
            public const string AppDisplayOrder = "appDisplayOrder";
            public const string AppWebsite = "appWebsite";
            public const string AppContactNo = "appContactNo";
            public const string AppEmail = "appEmail";
            public const string AppCODRate = "appCODRate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppCourierCompanyID] = _tblCourierCompany.PropertyNames.AppCourierCompanyID;
					ht[AppCourierCompany] = _tblCourierCompany.PropertyNames.AppCourierCompany;
					ht[AppIsActive] = _tblCourierCompany.PropertyNames.AppIsActive;
					ht[AppDisplayOrder] = _tblCourierCompany.PropertyNames.AppDisplayOrder;
					ht[AppWebsite] = _tblCourierCompany.PropertyNames.AppWebsite;
					ht[AppContactNo] = _tblCourierCompany.PropertyNames.AppContactNo;
					ht[AppEmail] = _tblCourierCompany.PropertyNames.AppEmail;
					ht[AppCODRate] = _tblCourierCompany.PropertyNames.AppCODRate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string AppCourierCompanyID = "AppCourierCompanyID";
            public const string AppCourierCompany = "AppCourierCompany";
            public const string AppIsActive = "AppIsActive";
            public const string AppDisplayOrder = "AppDisplayOrder";
            public const string AppWebsite = "AppWebsite";
            public const string AppContactNo = "AppContactNo";
            public const string AppEmail = "AppEmail";
            public const string AppCODRate = "AppCODRate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppCourierCompanyID] = _tblCourierCompany.ColumnNames.AppCourierCompanyID;
					ht[AppCourierCompany] = _tblCourierCompany.ColumnNames.AppCourierCompany;
					ht[AppIsActive] = _tblCourierCompany.ColumnNames.AppIsActive;
					ht[AppDisplayOrder] = _tblCourierCompany.ColumnNames.AppDisplayOrder;
					ht[AppWebsite] = _tblCourierCompany.ColumnNames.AppWebsite;
					ht[AppContactNo] = _tblCourierCompany.ColumnNames.AppContactNo;
					ht[AppEmail] = _tblCourierCompany.ColumnNames.AppEmail;
					ht[AppCODRate] = _tblCourierCompany.ColumnNames.AppCODRate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string AppCourierCompanyID = "s_AppCourierCompanyID";
            public const string AppCourierCompany = "s_AppCourierCompany";
            public const string AppIsActive = "s_AppIsActive";
            public const string AppDisplayOrder = "s_AppDisplayOrder";
            public const string AppWebsite = "s_AppWebsite";
            public const string AppContactNo = "s_AppContactNo";
            public const string AppEmail = "s_AppEmail";
            public const string AppCODRate = "s_AppCODRate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int AppCourierCompanyID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppCourierCompanyID);
			}
			set
	        {
				base.Setint(ColumnNames.AppCourierCompanyID, value);
			}
		}

		public virtual string AppCourierCompany
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppCourierCompany);
			}
			set
	        {
				base.Setstring(ColumnNames.AppCourierCompany, value);
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

		public virtual string AppWebsite
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppWebsite);
			}
			set
	        {
				base.Setstring(ColumnNames.AppWebsite, value);
			}
		}

		public virtual string AppContactNo
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppContactNo);
			}
			set
	        {
				base.Setstring(ColumnNames.AppContactNo, value);
			}
		}

		public virtual string AppEmail
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppEmail);
			}
			set
	        {
				base.Setstring(ColumnNames.AppEmail, value);
			}
		}

		public virtual decimal AppCODRate
	    {
			get
	        {
				return base.Getdecimal(ColumnNames.AppCODRate);
			}
			set
	        {
				base.Setdecimal(ColumnNames.AppCODRate, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_AppCourierCompanyID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppCourierCompanyID) ? string.Empty : base.GetintAsString(ColumnNames.AppCourierCompanyID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppCourierCompanyID);
				else
					this.AppCourierCompanyID = base.SetintAsString(ColumnNames.AppCourierCompanyID, value);
			}
		}

		public virtual string s_AppCourierCompany
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppCourierCompany) ? string.Empty : base.GetstringAsString(ColumnNames.AppCourierCompany);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppCourierCompany);
				else
					this.AppCourierCompany = base.SetstringAsString(ColumnNames.AppCourierCompany, value);
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

		public virtual string s_AppWebsite
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppWebsite) ? string.Empty : base.GetstringAsString(ColumnNames.AppWebsite);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppWebsite);
				else
					this.AppWebsite = base.SetstringAsString(ColumnNames.AppWebsite, value);
			}
		}

		public virtual string s_AppContactNo
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppContactNo) ? string.Empty : base.GetstringAsString(ColumnNames.AppContactNo);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppContactNo);
				else
					this.AppContactNo = base.SetstringAsString(ColumnNames.AppContactNo, value);
			}
		}

		public virtual string s_AppEmail
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppEmail) ? string.Empty : base.GetstringAsString(ColumnNames.AppEmail);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppEmail);
				else
					this.AppEmail = base.SetstringAsString(ColumnNames.AppEmail, value);
			}
		}

		public virtual string s_AppCODRate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppCODRate) ? string.Empty : base.GetdecimalAsString(ColumnNames.AppCODRate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppCODRate);
				else
					this.AppCODRate = base.SetdecimalAsString(ColumnNames.AppCODRate, value);
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
				
				
				public WhereParameter AppCourierCompanyID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppCourierCompanyID, Parameters.AppCourierCompanyID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppCourierCompany
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppCourierCompany, Parameters.AppCourierCompany);
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

				public WhereParameter AppWebsite
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppWebsite, Parameters.AppWebsite);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppContactNo
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppContactNo, Parameters.AppContactNo);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppEmail
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppEmail, Parameters.AppEmail);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppCODRate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppCODRate, Parameters.AppCODRate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter AppCourierCompanyID
		    {
				get
		        {
					if(_AppCourierCompanyID_W == null)
	        	    {
						_AppCourierCompanyID_W = TearOff.AppCourierCompanyID;
					}
					return _AppCourierCompanyID_W;
				}
			}

			public WhereParameter AppCourierCompany
		    {
				get
		        {
					if(_AppCourierCompany_W == null)
	        	    {
						_AppCourierCompany_W = TearOff.AppCourierCompany;
					}
					return _AppCourierCompany_W;
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

			public WhereParameter AppWebsite
		    {
				get
		        {
					if(_AppWebsite_W == null)
	        	    {
						_AppWebsite_W = TearOff.AppWebsite;
					}
					return _AppWebsite_W;
				}
			}

			public WhereParameter AppContactNo
		    {
				get
		        {
					if(_AppContactNo_W == null)
	        	    {
						_AppContactNo_W = TearOff.AppContactNo;
					}
					return _AppContactNo_W;
				}
			}

			public WhereParameter AppEmail
		    {
				get
		        {
					if(_AppEmail_W == null)
	        	    {
						_AppEmail_W = TearOff.AppEmail;
					}
					return _AppEmail_W;
				}
			}

			public WhereParameter AppCODRate
		    {
				get
		        {
					if(_AppCODRate_W == null)
	        	    {
						_AppCODRate_W = TearOff.AppCODRate;
					}
					return _AppCODRate_W;
				}
			}

			private WhereParameter _AppCourierCompanyID_W = null;
			private WhereParameter _AppCourierCompany_W = null;
			private WhereParameter _AppIsActive_W = null;
			private WhereParameter _AppDisplayOrder_W = null;
			private WhereParameter _AppWebsite_W = null;
			private WhereParameter _AppContactNo_W = null;
			private WhereParameter _AppEmail_W = null;
			private WhereParameter _AppCODRate_W = null;

			public void WhereClauseReset()
			{
				_AppCourierCompanyID_W = null;
				_AppCourierCompany_W = null;
				_AppIsActive_W = null;
				_AppDisplayOrder_W = null;
				_AppWebsite_W = null;
				_AppContactNo_W = null;
				_AppEmail_W = null;
				_AppCODRate_W = null;

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
				
				
				public AggregateParameter AppCourierCompanyID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppCourierCompanyID, Parameters.AppCourierCompanyID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppCourierCompany
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppCourierCompany, Parameters.AppCourierCompany);
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

				public AggregateParameter AppWebsite
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppWebsite, Parameters.AppWebsite);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppContactNo
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppContactNo, Parameters.AppContactNo);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppEmail
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppEmail, Parameters.AppEmail);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppCODRate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppCODRate, Parameters.AppCODRate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter AppCourierCompanyID
		    {
				get
		        {
					if(_AppCourierCompanyID_W == null)
	        	    {
						_AppCourierCompanyID_W = TearOff.AppCourierCompanyID;
					}
					return _AppCourierCompanyID_W;
				}
			}

			public AggregateParameter AppCourierCompany
		    {
				get
		        {
					if(_AppCourierCompany_W == null)
	        	    {
						_AppCourierCompany_W = TearOff.AppCourierCompany;
					}
					return _AppCourierCompany_W;
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

			public AggregateParameter AppWebsite
		    {
				get
		        {
					if(_AppWebsite_W == null)
	        	    {
						_AppWebsite_W = TearOff.AppWebsite;
					}
					return _AppWebsite_W;
				}
			}

			public AggregateParameter AppContactNo
		    {
				get
		        {
					if(_AppContactNo_W == null)
	        	    {
						_AppContactNo_W = TearOff.AppContactNo;
					}
					return _AppContactNo_W;
				}
			}

			public AggregateParameter AppEmail
		    {
				get
		        {
					if(_AppEmail_W == null)
	        	    {
						_AppEmail_W = TearOff.AppEmail;
					}
					return _AppEmail_W;
				}
			}

			public AggregateParameter AppCODRate
		    {
				get
		        {
					if(_AppCODRate_W == null)
	        	    {
						_AppCODRate_W = TearOff.AppCODRate;
					}
					return _AppCODRate_W;
				}
			}

			private AggregateParameter _AppCourierCompanyID_W = null;
			private AggregateParameter _AppCourierCompany_W = null;
			private AggregateParameter _AppIsActive_W = null;
			private AggregateParameter _AppDisplayOrder_W = null;
			private AggregateParameter _AppWebsite_W = null;
			private AggregateParameter _AppContactNo_W = null;
			private AggregateParameter _AppEmail_W = null;
			private AggregateParameter _AppCODRate_W = null;

			public void AggregateClauseReset()
			{
				_AppCourierCompanyID_W = null;
				_AppCourierCompany_W = null;
				_AppIsActive_W = null;
				_AppDisplayOrder_W = null;
				_AppWebsite_W = null;
				_AppContactNo_W = null;
				_AppEmail_W = null;
				_AppCODRate_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblCourierCompanyInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.AppCourierCompanyID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblCourierCompanyUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblCourierCompanyDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.AppCourierCompanyID);
			p.SourceColumn = ColumnNames.AppCourierCompanyID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.AppCourierCompanyID);
			p.SourceColumn = ColumnNames.AppCourierCompanyID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppCourierCompany);
			p.SourceColumn = ColumnNames.AppCourierCompany;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppIsActive);
			p.SourceColumn = ColumnNames.AppIsActive;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppDisplayOrder);
			p.SourceColumn = ColumnNames.AppDisplayOrder;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppWebsite);
			p.SourceColumn = ColumnNames.AppWebsite;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppContactNo);
			p.SourceColumn = ColumnNames.AppContactNo;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppEmail);
			p.SourceColumn = ColumnNames.AppEmail;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppCODRate);
			p.SourceColumn = ColumnNames.AppCODRate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}