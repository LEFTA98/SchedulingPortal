using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Net;
using GotDotNet.ApplicationBlocks.Data;
using RotmanTrading.Business;

namespace RotmanTrading.Business
{
    /// <summary>
    /// Developed by ESP Technologies (www.ESPTech.com).
    /// </summary>
    public class Transaction : BaseBusiness
	{

		private const string SP_SET_USER = "P_WEB_SIGNON.SetUser";

		private IDbConnection m_objConn = null;
		private IDbTransaction m_objTransaction = null;

		public Transaction(string strConnect,AdoHelper dbHelper)
		{
            m_objConn = dbHelper.GetConnection(strConnect);
        }

        public Transaction(AdoHelper dbHelper)
		{
            m_objConn = dbHelper.GetConnection(m_conROTMAN);
        }

		/// Method: TransactionBegin
		/// Begins a transaction.
		/// Inputs:
		///		(none)
		///	Outputs:
		///		(none)
		/// </summary>
		public IDbTransaction TransactionBegin()
		{
			m_objConn.Open();
			m_objTransaction = m_objConn.BeginTransaction();
			return m_objTransaction;
		}	//end TransactionBegin

		public void TransactionBegin(string setUserName)
		{
			m_objConn.Open();
			m_objTransaction = m_objConn.BeginTransaction();
		}

		/// Method: TransactionCommit
		/// Commits a transaction.
		/// Inputs:
		///		(none)
		///	Outputs:
		///		(none)
		/// </summary>
		public void TransactionCommit()
		{
			if (m_objTransaction != null)
			{
				m_objTransaction.Commit();
				m_objTransaction = null;
			}
			if (m_objConn != null)
			{
				m_objConn.Close();
				m_objConn = null;
			}
		}	//end TransactionCommit

		/// <summary>
		/// Method: TransactionRollback
		/// Rolls back a transaction.
		/// Inputs:
		///		(none)
		///	Outputs:
		///		(none)
		/// </summary>
		public void TransactionRollback()
		{
			if (m_objTransaction != null)
			{
				m_objTransaction.Rollback();
				m_objTransaction.Dispose();
				m_objTransaction = null;
			}
			if (m_objConn != null)
			{
				m_objConn.Close();
				m_objConn.Dispose();
				m_objConn = null;
			}
		}	//end TransactionRollback

		public IDbTransaction TransactionInterface
		{
			get
			{
				return m_objTransaction;
			}
		}

	}	//end class Transaction
}	//end namespace
