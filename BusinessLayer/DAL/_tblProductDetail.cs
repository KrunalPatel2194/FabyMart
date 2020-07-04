
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
	public abstract class _tblProductDetail : SqlClientEntity
	{
		public _tblProductDetail()
		{
			this.QuerySource = "tblProductDetail";
			this.MappingName = "tblProductDetail";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblProductDetailLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int AppProductDetailID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.AppProductDetailID, AppProductDetailID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblProductDetailLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter AppProductDetailID
			{
				get
				{
					return new SqlParameter("@AppProductDetailID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppProductColorID
			{
				get
				{
					return new SqlParameter("@AppProductColorID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppSellerPrice
			{
				get
				{
					return new SqlParameter("@AppSellerPrice", SqlDbType.Decimal, 0);
				}
			}
			
			public static SqlParameter AppMRP
			{
				get
				{
					return new SqlParameter("@AppMRP", SqlDbType.Decimal, 0);
				}
			}
			
			public static SqlParameter AppPrice
			{
				get
				{
					return new SqlParameter("@AppPrice", SqlDbType.Decimal, 0);
				}
			}
			
			public static SqlParameter AppQuantity
			{
				get
				{
					return new SqlParameter("@AppQuantity", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppSKUNo
			{
				get
				{
					return new SqlParameter("@AppSKUNo", SqlDbType.VarChar, 50);
				}
			}
			
			public static SqlParameter AppSizeID
			{
				get
				{
					return new SqlParameter("@AppSizeID", SqlDbType.Int, 0);
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
            public const string AppProductDetailID = "appProductDetailID";
            public const string AppProductColorID = "appProductColorID";
            public const string AppSellerPrice = "appSellerPrice";
            public const string AppMRP = "appMRP";
            public const string AppPrice = "appPrice";
            public const string AppQuantity = "appQuantity";
            public const string AppSKUNo = "appSKUNo";
            public const string AppSizeID = "appSizeID";
            public const string AppIsDefault = "appIsDefault";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppProductDetailID] = _tblProductDetail.PropertyNames.AppProductDetailID;
					ht[AppProductColorID] = _tblProductDetail.PropertyNames.AppProductColorID;
					ht[AppSellerPrice] = _tblProductDetail.PropertyNames.AppSellerPrice;
					ht[AppMRP] = _tblProductDetail.PropertyNames.AppMRP;
					ht[AppPrice] = _tblProductDetail.PropertyNames.AppPrice;
					ht[AppQuantity] = _tblProductDetail.PropertyNames.AppQuantity;
					ht[AppSKUNo] = _tblProductDetail.PropertyNames.AppSKUNo;
					ht[AppSizeID] = _tblProductDetail.PropertyNames.AppSizeID;
					ht[AppIsDefault] = _tblProductDetail.PropertyNames.AppIsDefault;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string AppProductDetailID = "AppProductDetailID";
            public const string AppProductColorID = "AppProductColorID";
            public const string AppSellerPrice = "AppSellerPrice";
            public const string AppMRP = "AppMRP";
            public const string AppPrice = "AppPrice";
            public const string AppQuantity = "AppQuantity";
            public const string AppSKUNo = "AppSKUNo";
            public const string AppSizeID = "AppSizeID";
            public const string AppIsDefault = "AppIsDefault";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppProductDetailID] = _tblProductDetail.ColumnNames.AppProductDetailID;
					ht[AppProductColorID] = _tblProductDetail.ColumnNames.AppProductColorID;
					ht[AppSellerPrice] = _tblProductDetail.ColumnNames.AppSellerPrice;
					ht[AppMRP] = _tblProductDetail.ColumnNames.AppMRP;
					ht[AppPrice] = _tblProductDetail.ColumnNames.AppPrice;
					ht[AppQuantity] = _tblProductDetail.ColumnNames.AppQuantity;
					ht[AppSKUNo] = _tblProductDetail.ColumnNames.AppSKUNo;
					ht[AppSizeID] = _tblProductDetail.ColumnNames.AppSizeID;
					ht[AppIsDefault] = _tblProductDetail.ColumnNames.AppIsDefault;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string AppProductDetailID = "s_AppProductDetailID";
            public const string AppProductColorID = "s_AppProductColorID";
            public const string AppSellerPrice = "s_AppSellerPrice";
            public const string AppMRP = "s_AppMRP";
            public const string AppPrice = "s_AppPrice";
            public const string AppQuantity = "s_AppQuantity";
            public const string AppSKUNo = "s_AppSKUNo";
            public const string AppSizeID = "s_AppSizeID";
            public const string AppIsDefault = "s_AppIsDefault";

		}
		#endregion		
		
		#region Properties
	
		public virtual int AppProductDetailID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppProductDetailID);
			}
			set
	        {
				base.Setint(ColumnNames.AppProductDetailID, value);
			}
		}

		public virtual int AppProductColorID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppProductColorID);
			}
			set
	        {
				base.Setint(ColumnNames.AppProductColorID, value);
			}
		}

		public virtual decimal AppSellerPrice
	    {
			get
	        {
				return base.Getdecimal(ColumnNames.AppSellerPrice);
			}
			set
	        {
				base.Setdecimal(ColumnNames.AppSellerPrice, value);
			}
		}

		public virtual decimal AppMRP
	    {
			get
	        {
				return base.Getdecimal(ColumnNames.AppMRP);
			}
			set
	        {
				base.Setdecimal(ColumnNames.AppMRP, value);
			}
		}

		public virtual decimal AppPrice
	    {
			get
	        {
				return base.Getdecimal(ColumnNames.AppPrice);
			}
			set
	        {
				base.Setdecimal(ColumnNames.AppPrice, value);
			}
		}

		public virtual int AppQuantity
	    {
			get
	        {
				return base.Getint(ColumnNames.AppQuantity);
			}
			set
	        {
				base.Setint(ColumnNames.AppQuantity, value);
			}
		}

		public virtual string AppSKUNo
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppSKUNo);
			}
			set
	        {
				base.Setstring(ColumnNames.AppSKUNo, value);
			}
		}

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
	
		public virtual string s_AppProductDetailID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppProductDetailID) ? string.Empty : base.GetintAsString(ColumnNames.AppProductDetailID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppProductDetailID);
				else
					this.AppProductDetailID = base.SetintAsString(ColumnNames.AppProductDetailID, value);
			}
		}

		public virtual string s_AppProductColorID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppProductColorID) ? string.Empty : base.GetintAsString(ColumnNames.AppProductColorID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppProductColorID);
				else
					this.AppProductColorID = base.SetintAsString(ColumnNames.AppProductColorID, value);
			}
		}

		public virtual string s_AppSellerPrice
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppSellerPrice) ? string.Empty : base.GetdecimalAsString(ColumnNames.AppSellerPrice);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppSellerPrice);
				else
					this.AppSellerPrice = base.SetdecimalAsString(ColumnNames.AppSellerPrice, value);
			}
		}

		public virtual string s_AppMRP
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppMRP) ? string.Empty : base.GetdecimalAsString(ColumnNames.AppMRP);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppMRP);
				else
					this.AppMRP = base.SetdecimalAsString(ColumnNames.AppMRP, value);
			}
		}

		public virtual string s_AppPrice
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppPrice) ? string.Empty : base.GetdecimalAsString(ColumnNames.AppPrice);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppPrice);
				else
					this.AppPrice = base.SetdecimalAsString(ColumnNames.AppPrice, value);
			}
		}

		public virtual string s_AppQuantity
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppQuantity) ? string.Empty : base.GetintAsString(ColumnNames.AppQuantity);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppQuantity);
				else
					this.AppQuantity = base.SetintAsString(ColumnNames.AppQuantity, value);
			}
		}

		public virtual string s_AppSKUNo
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppSKUNo) ? string.Empty : base.GetstringAsString(ColumnNames.AppSKUNo);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppSKUNo);
				else
					this.AppSKUNo = base.SetstringAsString(ColumnNames.AppSKUNo, value);
			}
		}

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
				
				
				public WhereParameter AppProductDetailID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppProductDetailID, Parameters.AppProductDetailID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppProductColorID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppProductColorID, Parameters.AppProductColorID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppSellerPrice
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppSellerPrice, Parameters.AppSellerPrice);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppMRP
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppMRP, Parameters.AppMRP);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppPrice
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppPrice, Parameters.AppPrice);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppQuantity
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppQuantity, Parameters.AppQuantity);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppSKUNo
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppSKUNo, Parameters.AppSKUNo);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
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
		
			public WhereParameter AppProductDetailID
		    {
				get
		        {
					if(_AppProductDetailID_W == null)
	        	    {
						_AppProductDetailID_W = TearOff.AppProductDetailID;
					}
					return _AppProductDetailID_W;
				}
			}

			public WhereParameter AppProductColorID
		    {
				get
		        {
					if(_AppProductColorID_W == null)
	        	    {
						_AppProductColorID_W = TearOff.AppProductColorID;
					}
					return _AppProductColorID_W;
				}
			}

			public WhereParameter AppSellerPrice
		    {
				get
		        {
					if(_AppSellerPrice_W == null)
	        	    {
						_AppSellerPrice_W = TearOff.AppSellerPrice;
					}
					return _AppSellerPrice_W;
				}
			}

			public WhereParameter AppMRP
		    {
				get
		        {
					if(_AppMRP_W == null)
	        	    {
						_AppMRP_W = TearOff.AppMRP;
					}
					return _AppMRP_W;
				}
			}

			public WhereParameter AppPrice
		    {
				get
		        {
					if(_AppPrice_W == null)
	        	    {
						_AppPrice_W = TearOff.AppPrice;
					}
					return _AppPrice_W;
				}
			}

			public WhereParameter AppQuantity
		    {
				get
		        {
					if(_AppQuantity_W == null)
	        	    {
						_AppQuantity_W = TearOff.AppQuantity;
					}
					return _AppQuantity_W;
				}
			}

			public WhereParameter AppSKUNo
		    {
				get
		        {
					if(_AppSKUNo_W == null)
	        	    {
						_AppSKUNo_W = TearOff.AppSKUNo;
					}
					return _AppSKUNo_W;
				}
			}

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

			private WhereParameter _AppProductDetailID_W = null;
			private WhereParameter _AppProductColorID_W = null;
			private WhereParameter _AppSellerPrice_W = null;
			private WhereParameter _AppMRP_W = null;
			private WhereParameter _AppPrice_W = null;
			private WhereParameter _AppQuantity_W = null;
			private WhereParameter _AppSKUNo_W = null;
			private WhereParameter _AppSizeID_W = null;
			private WhereParameter _AppIsDefault_W = null;

			public void WhereClauseReset()
			{
				_AppProductDetailID_W = null;
				_AppProductColorID_W = null;
				_AppSellerPrice_W = null;
				_AppMRP_W = null;
				_AppPrice_W = null;
				_AppQuantity_W = null;
				_AppSKUNo_W = null;
				_AppSizeID_W = null;
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
				
				
				public AggregateParameter AppProductDetailID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppProductDetailID, Parameters.AppProductDetailID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppProductColorID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppProductColorID, Parameters.AppProductColorID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppSellerPrice
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppSellerPrice, Parameters.AppSellerPrice);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppMRP
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppMRP, Parameters.AppMRP);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppPrice
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppPrice, Parameters.AppPrice);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppQuantity
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppQuantity, Parameters.AppQuantity);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppSKUNo
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppSKUNo, Parameters.AppSKUNo);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
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
		
			public AggregateParameter AppProductDetailID
		    {
				get
		        {
					if(_AppProductDetailID_W == null)
	        	    {
						_AppProductDetailID_W = TearOff.AppProductDetailID;
					}
					return _AppProductDetailID_W;
				}
			}

			public AggregateParameter AppProductColorID
		    {
				get
		        {
					if(_AppProductColorID_W == null)
	        	    {
						_AppProductColorID_W = TearOff.AppProductColorID;
					}
					return _AppProductColorID_W;
				}
			}

			public AggregateParameter AppSellerPrice
		    {
				get
		        {
					if(_AppSellerPrice_W == null)
	        	    {
						_AppSellerPrice_W = TearOff.AppSellerPrice;
					}
					return _AppSellerPrice_W;
				}
			}

			public AggregateParameter AppMRP
		    {
				get
		        {
					if(_AppMRP_W == null)
	        	    {
						_AppMRP_W = TearOff.AppMRP;
					}
					return _AppMRP_W;
				}
			}

			public AggregateParameter AppPrice
		    {
				get
		        {
					if(_AppPrice_W == null)
	        	    {
						_AppPrice_W = TearOff.AppPrice;
					}
					return _AppPrice_W;
				}
			}

			public AggregateParameter AppQuantity
		    {
				get
		        {
					if(_AppQuantity_W == null)
	        	    {
						_AppQuantity_W = TearOff.AppQuantity;
					}
					return _AppQuantity_W;
				}
			}

			public AggregateParameter AppSKUNo
		    {
				get
		        {
					if(_AppSKUNo_W == null)
	        	    {
						_AppSKUNo_W = TearOff.AppSKUNo;
					}
					return _AppSKUNo_W;
				}
			}

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

			private AggregateParameter _AppProductDetailID_W = null;
			private AggregateParameter _AppProductColorID_W = null;
			private AggregateParameter _AppSellerPrice_W = null;
			private AggregateParameter _AppMRP_W = null;
			private AggregateParameter _AppPrice_W = null;
			private AggregateParameter _AppQuantity_W = null;
			private AggregateParameter _AppSKUNo_W = null;
			private AggregateParameter _AppSizeID_W = null;
			private AggregateParameter _AppIsDefault_W = null;

			public void AggregateClauseReset()
			{
				_AppProductDetailID_W = null;
				_AppProductColorID_W = null;
				_AppSellerPrice_W = null;
				_AppMRP_W = null;
				_AppPrice_W = null;
				_AppQuantity_W = null;
				_AppSKUNo_W = null;
				_AppSizeID_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductDetailInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.AppProductDetailID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductDetailUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductDetailDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.AppProductDetailID);
			p.SourceColumn = ColumnNames.AppProductDetailID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.AppProductDetailID);
			p.SourceColumn = ColumnNames.AppProductDetailID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppProductColorID);
			p.SourceColumn = ColumnNames.AppProductColorID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppSellerPrice);
			p.SourceColumn = ColumnNames.AppSellerPrice;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppMRP);
			p.SourceColumn = ColumnNames.AppMRP;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppPrice);
			p.SourceColumn = ColumnNames.AppPrice;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppQuantity);
			p.SourceColumn = ColumnNames.AppQuantity;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppSKUNo);
			p.SourceColumn = ColumnNames.AppSKUNo;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppSizeID);
			p.SourceColumn = ColumnNames.AppSizeID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppIsDefault);
			p.SourceColumn = ColumnNames.AppIsDefault;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
