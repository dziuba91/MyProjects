package client;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Container;
import java.awt.Graphics;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.ListSelectionModel;
import javax.swing.border.Border;
import javax.swing.border.CompoundBorder;
import javax.swing.border.EmptyBorder;
import javax.swing.border.LineBorder;
import javax.swing.table.TableColumnModel;
import javax.swing.table.TableModel;

public class GUIClient extends JFrame implements ActionListener {

	XMLRPCClient xmlrpcClient = null;
	Container contentPane;
	JTable table = null;
	JScrollPane spane;
	JLabel userNameLabel;
	JLabel passwordLabel;
	JLabel urlLabel;
	JLabel trustStoreLabel;
	JLabel queryLabel;
	JButton addButton;
	JTextField userNameTxtF;
	JPasswordField passwordF;
	JTextField urlTxtF;
	JTextField trustStoreTxtF;
	JTextField queryTxtF;
	JPanel textButtonPanel;
	TableModel tableModel = null;
	TableColumnModel tableColumnModel = null;

	Object removableComponent = null;
	JScrollPane scrollPane = null;

	boolean tableAdded = false;

	String[] colHeads = null;
	Object[][] rowData = null;
	Graphics g = null;
	
	
	// adding
	JPanel optionPanel;
	
	JPanel dataPanel;
	JTextField idTxtF;
	JTextField guestNameTxtF;
	JTextField commentTxtF;
	JButton addToDataBaseButton;
	
	// delete
	JTextField idDeleteTxtF;
	JButton deleteButton;
	
	// search
	JTextField idSearchTxtF;
	JButton searchButton;
	
	JButton displayAllButton;

	
	String[] queryRows;
	
	
	public GUIClient() {
		super("Table demo");

		contentPane = this.getContentPane();

		scrollPane = new JScrollPane();
		textButtonPanel = new JPanel();
		textButtonPanel.setLayout(new GridLayout(5, 2));

		userNameLabel = new JLabel("User name ", JLabel.LEFT);
		passwordLabel = new JLabel("Password ", JLabel.LEFT);
		urlLabel = new JLabel("Server URL ", JLabel.LEFT);
		//trustStoreLabel = new JLabel("Truststore file ", JLabel.LEFT);
		//queryLabel = new JLabel("SQL Query", JLabel.LEFT);

		userNameTxtF = new JTextField("tomcat");
		passwordF = new JPasswordField("tomcat");

		urlTxtF = new JTextField(
				"http://app3.cc.puv.fi/e1401209_exam1_10/db_xmlrpc");
		//trustStoreTxtF = new JTextField("C:/truststore");

		addButton = new JButton("Add table");
		addButton.setForeground(Color.BLACK);
		addButton.setBackground(Color.green);
		Border line = new LineBorder(Color.BLACK);
		Border margin = new EmptyBorder(5, 15, 5, 15);
		Border compound = new CompoundBorder(line, margin);
		addButton.setBorder(compound);

		addButton.addActionListener(this);

		
		// changes 
		// adding
		optionPanel = new JPanel();
		optionPanel.setLayout(new GridLayout(2, 1));
		
		dataPanel = new JPanel();
		dataPanel.setLayout(new GridLayout(3, 2));
		
		idTxtF = new JTextField();
		guestNameTxtF = new JTextField();
		commentTxtF = new JTextField();
		addToDataBaseButton = new JButton("ADD");
		
		addToDataBaseButton.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent arg0) {

				String query = "insert into guestbook (name, comment, date) values (?, ?, ?)";
					
				xmlrpcClient = new XMLRPCClient(urlTxtF.getText(),
						userNameTxtF.getText(), new String(passwordF.getPassword()));
				
				Boolean queryResult = false;
				
				DateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
				Date date = new Date();
				String getDate = dateFormat.format(date);
				
				try
				{
					queryResult = xmlrpcClient.dbHanlder.executeInsertQuery(query, guestNameTxtF.getText(), commentTxtF.getText(), getDate);
				}
				catch (Exception e)
				{
					System.out.println("Parameters not set properly!");
				}
				
				if (queryResult == null || queryResult == false) {

					System.out.println("Insert query execution problem!");
					
					return;
				}
				
				System.out.println("Insert query result : " + queryResult);
				
				actionPerformed(new ActionEvent(displayAllButton, 0, "Add"));
			}

			@Override
			public void mouseEntered(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseExited(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mousePressed(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseReleased(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}});
		
		displayAllButton = new JButton("DISP. ALL GUEST");
		
		displayAllButton.addActionListener(this);
		
		/*
		displayAllButton.addMouseListener(new MouseListener(){

			@Override
			public void mouseClicked(MouseEvent e) {
				
				String query = "select * from product";
				
				queryTxtF.setText(query);
				
				actionPerformed(new ActionEvent(addButton, 0, "Add"));
			}

			@Override
			public void mouseEntered(MouseEvent e) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseExited(MouseEvent e) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mousePressed(MouseEvent e) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void mouseReleased(MouseEvent e) {
				// TODO Auto-generated method stub
				
			}
			
		});
		*/
		
		//dataPanel.add(new JLabel("ID", JLabel.CENTER));
		dataPanel.add(new JLabel("NAME", JLabel.CENTER));
		dataPanel.add(new JLabel("COMMENT", JLabel.CENTER));
		dataPanel.add(new JLabel());
		
		//dataPanel.add(idTxtF);
		dataPanel.add(guestNameTxtF);
		dataPanel.add(commentTxtF);
		dataPanel.add(addToDataBaseButton);
		
		//dataPanel.add(new JLabel());
		//dataPanel.add(new JLabel("Delete by ID:   ", JLabel.RIGHT));
		//dataPanel.add(idDeleteTxtF);
		//dataPanel.add(deleteButton);
		
		//dataPanel.add(new JLabel());
		//dataPanel.add(new JLabel("Search by ID:   ", JLabel.RIGHT));
		//dataPanel.add(idSearchTxtF);
		//dataPanel.add(searchButton);
		
		//dataPanel.add(new JLabel());
		dataPanel.add(new JLabel());
		dataPanel.add(new JLabel());
		dataPanel.add(displayAllButton);
		/*
		dataPanel.add(new JLabel());
		dataPanel.add(new JLabel());
		dataPanel.add(new JLabel());
		
		dataPanel.add(new JLabel());
		dataPanel.add(new JLabel());
		dataPanel.add(new JLabel());
		
		dataPanel.add(new JLabel());
		dataPanel.add(new JLabel());
		dataPanel.add(new JLabel());
		*/
		textButtonPanel.add(userNameLabel);
		textButtonPanel.add(userNameTxtF);
		textButtonPanel.add(passwordLabel);
		textButtonPanel.add(passwordF);
		textButtonPanel.add(urlLabel);
		textButtonPanel.add(urlTxtF);
		//textButtonPanel.add(queryLabel);
		
		//textButtonPanel.add(trustStoreLabel);
		//textButtonPanel.add(trustStoreTxtF);

		textButtonPanel.add(new JLabel());
		
		
		optionPanel.add(textButtonPanel);
		optionPanel.add(dataPanel);
		//textButtonPanel.add(addingDataPanel);

		contentPane.add(optionPanel, BorderLayout.SOUTH);

		setBackground(Color.cyan);

		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		setSize(600, 400);

		// Here we make the frame appear in the center of the window.
		setLocationRelativeTo(null);
		setVisible(true);
	}

	@Override
	public void actionPerformed(ActionEvent e) {

		contentPane.remove(scrollPane);
		repaint();

		if (urlTxtF.getText().length() == 0
				|| userNameTxtF.getText().length() == 0
				|| passwordF.getPassword().length == 0) {
			JOptionPane.showMessageDialog(null, "Some fields are empty!",
					"Error", JOptionPane.ERROR_MESSAGE, null);

			return;
		}

		System.out.println(userNameTxtF.getText() + " -- "
				+ new String(passwordF.getPassword()));

		// System.out.println(queryTxtF.getText().length());

		xmlrpcClient = new XMLRPCClient(urlTxtF.getText(),
				userNameTxtF.getText(), new String(passwordF.getPassword()));
		
		//xmlrpcClient.dbHanlder.connectToDB("e1401209","fDD6H8shxWyQ");
		String queryResult = xmlrpcClient.executeQuery("select * from guestbook");
		

		if (queryResult == null) {

			// System.out.println(queryResult);
			scrollPane = new JScrollPane(new JTextArea("Exception occured1"));
			contentPane.add(scrollPane, BorderLayout.CENTER);
			g = scrollPane.getGraphics();
			paintAll(g);

			return;
		}

		queryRows = queryResult.split("\n");
		System.out.println("Query rows: " + queryRows.length);

		String[] headingColumns = queryRows[0].split("\t");

		System.out.println("Heading Cols: " + headingColumns.length);

		String[][] dataRows = new String[queryRows.length - 1][headingColumns.length];

		for (int i = 1; i < queryRows.length; i++)
			dataRows[i - 1] = queryRows[i].split("\t");

		table = new JTable(dataRows, headingColumns);

		table = new JTable(table.getModel());

		table.addMouseListener(new MouseListener() {

			@Override
			public void mouseClicked(MouseEvent e) {

				/*
				 * if (table.getSelectedColumn() >= 0 && table.getSelectedRow()
				 * >= 0) label.setText("Selected: " + (String)
				 * table.getValueAt(table.getSelectedRow(),
				 * table.getSelectedColumn()));
				 */

			}

			@Override
			public void mouseEntered(MouseEvent e) {
			}

			@Override
			public void mouseExited(MouseEvent e) {
			}

			@Override
			public void mousePressed(MouseEvent e) {
			}

			@Override
			public void mouseReleased(MouseEvent e) {

			}

		});

		table.setCellSelectionEnabled(true);

		ListSelectionModel cellSelectionModel = table.getSelectionModel();

		cellSelectionModel
				.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);

		scrollPane = new JScrollPane(table);
		contentPane.add(scrollPane, BorderLayout.CENTER);

		g = scrollPane.getGraphics();

		paintAll(g);

	}

	public static void main(String[] args) {

		new GUIClient();

	}

}